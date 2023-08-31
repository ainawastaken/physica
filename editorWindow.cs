using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Numerics;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Diagnostics;
using physica;
using System.Threading;
using physica.engine;
using physica.console;
using System.Reflection;
using OpenCL.Net.Extensions;
using OpenCL.Net;
using System.IO;
using physica.projWind;
using System.Threading.Tasks;
using System.Windows.Forms;

#pragma warning disable CS0169
#pragma warning disable CS0414
#pragma warning disable CS0649

namespace physica.editor
{
    public partial class editorWindow : Form
    {

        bool mouseOnCanvas = false;
        PointF trueMouseLocation;
        PointF trueMouseStart;
        PointF mouseLocation;
        PointF mouseStart;
        int sellectedTool = 0;
        bool mouseDown;

        bool scriptViewButtonHeldLMB;
        bool scriptViewButtonHeldRMB;
        bool scriptViewButtonHeldMMB;
        Point scriptViewMouseOffsetEarly;
        Point scriptViewMouseOffsetLate;

        bool consIsShown = false;

        Point canvasOffset;
        volatile Graphics canvasGraphics;

        Point openGlpreviewWindMousePos; 
        
        PointF closestPoint;
        int gridMul = 32;
        bool gridEnable = false;
        volatile bool gridSnapEnable = false;

        Thread ct;


        public editorWindow()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            var property = typeof(Control).GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            property.SetValue(canvas, true, null);
            property.SetValue(scriptTree, true, null);
            this.WindowState = FormWindowState.Maximized;


            scriptViewMouseOffsetLate = new Point(scriptTree.Width,0);

            Stopwatch sw = new Stopwatch();
            string progr = File.ReadAllText(@"Resources\gridFinder.cl");
            sw.Start();
            const int count = 2048;
            var random = new Random();
            float[] data = (from i in Enumerable.Range(0, count) select (float)random.NextDouble()).ToArray();
            Event event0; ErrorCode err;
            Platform[] platforms = Cl.GetPlatformIDs(out err);
            Device[] devices = Cl.GetDeviceIDs(platforms[0], DeviceType.Gpu, out err);
            Device device = devices[0]; //cl_device_id device;
            Context context = Cl.CreateContext(null, 1, devices, null, IntPtr.Zero, out err);
            CommandQueue cmdQueue = Cl.CreateCommandQueue(context, device, CommandQueueProperties.None, out err);
            OpenCL.Net.Program program = Cl.CreateProgramWithSource(context, 1, new[] { progr }, null, out err);
            Cl.BuildProgram(program, 0, null, string.Empty, null, IntPtr.Zero);  //"-cl-mad-enable"
            if (Cl.GetProgramBuildInfo(program, device, ProgramBuildInfo.Status, out err).CastTo<BuildStatus>() != BuildStatus.Success)
            {
                if (err != ErrorCode.Success) System.Console.WriteLine("ERROR: " + "Cl.GetProgramBuildInfo" + " (" + err.ToString() + ")");
                Program.c.print("Cl.GetProgramBuildInfo != Success");
                Program.c.print(Cl.GetProgramBuildInfo(program, device, ProgramBuildInfo.Log, out err).ToString());
            }
            Kernel kernel = Cl.CreateKernel(program, "doubleMe", out err);
            Mem memInput = (Mem)Cl.CreateBuffer(context, MemFlags.ReadOnly, sizeof(float) * count, out err);
            Mem memoutput = (Mem)Cl.CreateBuffer(context, MemFlags.WriteOnly, sizeof(float) * count, out err);
            Cl.EnqueueWriteBuffer(cmdQueue, (IMem)memInput, Bool.True, IntPtr.Zero, new IntPtr(sizeof(float) * count), data, 0, null, out event0);
            IntPtr notused;
            InfoBuffer local = new InfoBuffer(new IntPtr(4));
            Cl.GetKernelWorkGroupInfo(kernel, device, KernelWorkGroupInfo.WorkGroupSize, new IntPtr(sizeof(int)), local, out notused);
            Cl.SetKernelArg(kernel, 0, new IntPtr(4), memInput);
            Cl.SetKernelArg(kernel, 1, new IntPtr(4), memoutput);
            Cl.SetKernelArg(kernel, 2, new IntPtr(4), count);
            Cl.SetKernelArg(kernel, 3, new IntPtr(4), count);
            Cl.SetKernelArg(kernel, 4, new IntPtr(4), count);
            Cl.SetKernelArg(kernel, 5, new IntPtr(4), count);
            IntPtr[] workGroupSizePtr = new IntPtr[] { new IntPtr(count) };
            Cl.EnqueueNDRangeKernel(cmdQueue, kernel, 1, null, workGroupSizePtr, null, 0, null, out event0);
            Cl.Finish(cmdQueue);
            float[] results = new float[count];
            Cl.EnqueueReadBuffer(cmdQueue, (IMem)memoutput, Bool.True, IntPtr.Zero, new IntPtr(count * sizeof(float)), results, 0, null, out event0);
            int correct = 0;
            for (int i = 0; i < count; i++)correct += (results[i] == data[i] + data[i]) ? 1 : 0;
            sw.Stop();
            Program.c.print($"Computed {correct} of {count} correct values! In {sw.ElapsedMilliseconds} ms");
        }
        private void editorWindow_Resize(object sender, EventArgs e)
        {
            entitiesBox.Height = (this.Height / 2) - menuStrip1.Height;

            placedEntitiesBox.Height = (this.Height / 2) - menuStrip1.Height - 18;
            placedEntitiesBox.Location = new Point(0,(this.Height / 2) - menuStrip1.Height);

            offsetPanel.Location = new Point(tabControl1.SelectedTab.Width-77, 3);
            offsetPanel2.Location = new Point(tabControl1.SelectedTab.Width-230, 27);

            hidePanel1.Location = new Point(tabControl1.SelectedTab.Width-180, 3);
        }
        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            if (gridEnable)
            {
                for (int x = 0; x < canvas.Width; x += gridMul)
                {
                    for (int y = 0; y < canvas.Height; y += gridMul)
                    {
                        e.Graphics.DrawLine(Pens.LightGray, new Point(x,0), new Point(x,canvas.Height));
                        e.Graphics.DrawLine(Pens.LightGray, new Point(0, y), new Point(canvas.Width, y));
                    }
                }
            }

            if (mouseOnCanvas)
            {
                e.Graphics.DrawLine(Pens.Black, mouseLocation.X - 5, mouseLocation.Y, mouseLocation.X + 5, mouseLocation.Y);
                e.Graphics.DrawLine(Pens.Black, mouseLocation.X, mouseLocation.Y - 5, mouseLocation.X, mouseLocation.Y + 5);
                if (gridSnapEnable)
                {
                    e.Graphics.DrawLine(Pens.Red, mouseLocation, closestPoint);
                    e.Graphics.DrawLine(Pens.DarkBlue, closestPoint.X - 2.5f, closestPoint.Y - 2.5f,closestPoint.X+2.5f,closestPoint.Y+2.5f);
                    e.Graphics.DrawLine(Pens.DarkBlue, closestPoint.X + 2.5f, closestPoint.Y - 2.5f, closestPoint.X - 2.5f, closestPoint.Y + 2.5f);
                }
                

                physicaEngine.Tools.DrawTool(e.Graphics,trueMouseLocation,trueMouseStart,mouseDown,sellectedTool);
            }
            
        }
        private void canvas_MouseEnter(object sender, EventArgs e)
        {
            mouseOnCanvas = true;
            Cursor.Hide();
        }
        private void canvas_MouseLeave(object sender, EventArgs e)
        {
            mouseOnCanvas = false;   
            Cursor.Show();
        }
        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            mouseLocation = e.Location;
            canvas.Refresh();
            locationLabel.Text = $"Location: {trueMouseLocation.X},{trueMouseLocation.Y}";

            if (gridSnapEnable)
            {
                trueMouseLocation = new Point(physicaEngine.EMath.nearestMultiple(e.X, gridMul), physicaEngine.EMath.nearestMultiple(e.Y, gridMul));
            }
        }
        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                trueMouseStart = new Point(physicaEngine.EMath.nearestMultiple(e.X, gridMul), physicaEngine.EMath.nearestMultiple(e.Y, gridMul));

                mouseDown = true;
                if (sellectedTool == physicaEngine.Tools.ObjectTool)
                {
                    objectSellect1.Location = e.Location;
                    objectSellect1.Visible = true;
                    objectSellect1.Select();
                    objectSellect1.Focus();
                    objectSellect1.DroppedDown = true;
                }
            }
            else
            {
                trueMouseStart = e.Location;
            }
            if (gridSnapEnable)
            {
                trueMouseStart = closestPoint;
            }
        }
        private void canvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseDown = false;
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            canvas.Refresh();
        }
        private void canvas_Click(object sender, EventArgs e){}
        private void gridMultiplierPlus_Click(object sender, EventArgs e)
        {
            gridMul = gridMul*2;
            gridMulLabel.Text = $"Grid multiplier: {gridMul}";
        }
        private void gridMultiplierMin_Click(object sender, EventArgs e)
        {
            gridMul = gridMul / 2;
            gridMulLabel.Text = $"Grid multiplier: {gridMul}";
        }
        private void gridToggle1_Click(object sender, EventArgs e)
        {
            gridEnable = !gridEnable;
        }
        private void gridSnapToggle1_Click(object sender, EventArgs e)
        {
            gridSnapEnable = !gridSnapEnable;
        }
        private void editorWindow_FormClosed(object sender, FormClosedEventArgs e){}
        private void distanceTool1_Click(object sender, EventArgs e)
        {
            sellectedTool = physicaEngine.Tools.DistanceTool;
        }
        private void sellectTool1_Click(object sender, EventArgs e)
        {
            sellectedTool = physicaEngine.Tools.SellectTool;
        }
        private void newProjBtn_Click(object sender, EventArgs e)
        {
            projWind.projWind pw = new projWind.projWind();
            pw.tabControl12.SelectTab(1);
            pw.ShowDialog();
        }
        private void openProjBtn_Click(object sender, EventArgs e)
        {
            projWind.projWind pw = new projWind.projWind();
            pw.tabControl12.SelectTab(0);
            pw.ShowDialog();
        }
        private void scriptTree_MouseMove(object sender, MouseEventArgs e)
        {
            scriptTree.Refresh();
            Size sz = new Size(19, scriptTree.Height);
            Rectangle hitbox = new Rectangle(new Point(scriptTree.Width - sz.Width), sz);
            Graphics g = scriptTree.CreateGraphics();
            g.DrawRectangle(Pens.Black, hitbox);

            scriptViewMouseOffsetEarly = e.Location;

            if (hitbox.Contains(e.Location))
            {
                if (scriptViewButtonHeldLMB)
                {
                    hitbox.X = e.X - 7;
                    scriptTree.Width = (int)physicaEngine.EMath.midpoint(new PointF(scriptViewMouseOffsetEarly.X, 0f), new PointF(scriptViewMouseOffsetLate.X, 0f)).X + 7;
                    scriptViewMouseOffsetLate.X = (int)physicaEngine.EMath.midpoint(new PointF(scriptViewMouseOffsetEarly.X,0f), new PointF(scriptViewMouseOffsetLate.X, 0f)).X + 7;

                    scriptBox.Location = new Point((int)physicaEngine.EMath.midpoint(new PointF(scriptViewMouseOffsetEarly.X, 0f), new PointF(scriptViewMouseOffsetLate.X, 0f)).X + 7);
                    scriptBox.Width = documentMap1.Left;
                }
            }
            else
            {
                scriptTree.Refresh();
            }
            if (!scriptViewButtonHeldLMB)
            {

            }

            scriptBox.Text.Remove(scriptBox.Text.Length);
        }
        private void scriptTree_MouseDown(object sender, MouseEventArgs e)
        {
            scriptViewButtonHeldLMB = true;
        }
        private void scriptTree_MouseUp(object sender, MouseEventArgs e)
        {
            scriptViewButtonHeldLMB = false;
        }

        private void scriptTree_MouseLeave(object sender, EventArgs e)
        {
            scriptViewButtonHeldLMB = false;
            scriptTree.Refresh();

        private void createObjBtn_Click(object sender, EventArgs e)
        {
            sellectedTool = physicaEngine.Tools.ObjectTool;
        }
    }
}
#pragma warning restore CS0169
#pragma warning restore CS0414
#pragma warning restore CS0649

__kernel void findClosestGridPoint(__global float* XS, __global float* YS, __global float X, __global float Y, __global float OX, __global float OY) 
{
    float minDist = float.max;

    for (int i = 0; i < XS.fast_length; i++)
    {
        float dist = sqrt(pow(XS - X, 2) + pow(YS - Y, 2));

        if (dist < minDist)
        {
            minDist = dist;
            OX = XS[i];
            OY = YS[i];
        }
    }
}

/*
__kernel void findClosestGridPoint(__global float* XS, __global float* YS, __global float* XO, __global float* YO, int gm) 
{
    int i = 0;

    for (int x = 0; x < XS.fast_length + 1; x+=gm)
    {
        for (int y = 0; y < YS.fast_length + 1; y+=gm)
        {
            XO[i] = x;
            YO[i] = y;
            i++;
        }
    }
}
*/
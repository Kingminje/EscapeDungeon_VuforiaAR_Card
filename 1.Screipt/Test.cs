using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test
{
    public delegate double DobleInput(double a, double b);

    public double Plus(double a, double b)
    {
        return a + b;
    }

    public double Minus(double a, double b)
    {
        return a - b;
    }

    public double Calculator(double a, double b, DobleInput c)
    {
        return c(a, b);
    }

    private void Start()
    {
        DobleInput input;
        input = new DobleInput(Plus);
        input = new DobleInput(Minus);

        double a = Calculator(1, 3, Plus);
    }
}
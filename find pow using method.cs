given 2 int implement a function
to calcukate a^b
IP:a=2 b=3
OP:=8

using System;
					
public class Test
{
	public static double pow(double a , double b)
	{
		double result= Math.Pow(a, b);
		
		return result;
		
	}
	public static void Main()
	{
		double result;
		double x=Convert.ToDouble(Console.ReadLine()),y=Convert.ToDouble(Console.ReadLine());
		result = pow(x, y);
		Console.WriteLine(""+ result);
		
	}
}

//find the no closest to n and divisible by m
//if more than one output the max absolute  value 
//if n is completely divisible by m then output n only


using System;
					
public class Program
{
	public static int Main()
	{
		{
       int n2,n1;
       int n= Convert.ToInt32( Console.ReadLine()),m= Convert.ToInt32( Console.ReadLine());
        Console.WriteLine("the given numbers are=" + n +" ," + m);
			int p=Convert.ToInt32(n/m);
			 n1=m*p;
			if(n*p>0)
			{
				 n2=m*(p+1);
			}
			else
			{
				 n2 = m * (p - 1);
			}
			 if (Math.Abs(n-n1) < Math.Abs(n-n2))
			 {
				 
			Console.WriteLine("the closest no: " +n1);
				 return n1;
			 }
			else
			{
				
			Console.WriteLine("the closest no: " +n2);
				return n2; 
			}
		}
	}
}

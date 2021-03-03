using MathNet.Numerics.LinearAlgebra.Double;
using System;

namespace PerspectiveTransformation
{
	class Program
	{
		// Reference
		//http://www.hanyeah.com/blog/post/%E5%9B%9B%E9%A1%B6%E7%82%B9%E6%A0%A1%E6%AD%A3%E9%80%8F%E8%A7%86%E5%8F%98%E6%8D%A2%E7%9A%84%E7%BA%BF%E6%80%A7%E6%96%B9%E7%A8%8B%E8%A7%A3.html
		static void Main(string[] args)
		{
			const double width = 450;
			const double height = 250;

			////Source Point
			//double x1 = 0;
			//double y1 = 0;
			//double x2 = width;
			//double y2 = 0;
			//double x3 = width;
			//double y3 = height;
			//double x4 = 0;
			//double y4 = height;
			////Target Point
			//double u1 = 26;
			//double v1 = 197;
			//double u2 = 380;
			//double v2 = 51;
			//double u3 = 482;
			//double v3 = 116;
			//double u4 = 132;
			//double v4 = 287;

			//Source Point
			double x1 = 26;
			double y1 = 197;
			double x2 = 380;
			double y2 = 51;
			double x3 = 482;
			double y3 = 116;
			double x4 = 132;
			double y4 = 287;
			//Target Point
			double u1 = 0;
			double v1 = 0;
			double u2 = width;
			double v2 = 0;
			double u3 = width;
			double v3 = height;
			double u4 = 0;
			double v4 = height;


			var a = DenseMatrix.OfArray(new double[,]   {
					{ x1, y1, 1,  0,  0,  0,  -(x1 * u1), -(y1 * u1) },
					{ 0,  0,  0,  x1, y1, 1,  -(x1 * v1), -(y1 * v1) },
					{ x2, y2, 1,  0,  0,  0,  -(x2 * u2), -(y2 * u2) },
					{ 0,  0,  0,  x2, y2, 1,  -(x2 * v2), -(y2 * v2) },
					{ x3, y3, 1,  0,  0,  0,  -(x3 * u3), -(y3 * u3) },
					{ 0,  0,  0,  x3, y3, 1,  -(x3 * v3), -(y3 * v3) },
					{ x4, y4, 1,  0,  0,  0,  -(x4 * u4), -(y4 * u4) },
					{ 0,  0,  0,  x4, y4, 1,  -(x4 * v4), -(y4 * v4) }
				});

			var uVector = new DenseVector(new[] { u1, v1, u2, v2, u3, v3, u4, v4 });

			var matrixSourceToTarget = new DenseVector(8);

			a.GramSchmidt().Solve(uVector, matrixSourceToTarget);

			double scale = 1d;

			//Result
			Console.WriteLine($"{nameof(x1)} = {x1}, {nameof(y1)} = {y1}, {nameof(x2)} = {x2}, {nameof(y2)} = {y2}, {nameof(x3)} = {x3}, {nameof(y3)} = {y3}, {nameof(x4)} = {x4}, {nameof(y4)} = {y4}");
			Console.WriteLine($"{nameof(u1)} = {u1}, {nameof(v1)} = {v1}, {nameof(u2)} = {u2}, {nameof(v2)} = {v2}, {nameof(u3)} = {u3}, {nameof(v3)} = {v3}, {nameof(u4)} = {u4}, {nameof(v4)} = {v4}");

			Console.WriteLine(matrixSourceToTarget);

			Console.WriteLine($"Verify u2 = {( matrixSourceToTarget[0] * x2 + matrixSourceToTarget[1] * y2 + matrixSourceToTarget[2] ) / ( matrixSourceToTarget[6] * x2 + matrixSourceToTarget[7] * y2 + scale * 1 )}");
			Console.WriteLine($"Verify v1 = {( matrixSourceToTarget[3] * x1 + matrixSourceToTarget[4] * y1 + matrixSourceToTarget[5] ) / ( matrixSourceToTarget[6] * x1 + matrixSourceToTarget[7] * y1 + scale * 1 )}");

			//Inverse
			double[] result = new double[] { matrixSourceToTarget[0], matrixSourceToTarget[1], matrixSourceToTarget[2], matrixSourceToTarget[3], matrixSourceToTarget[4], matrixSourceToTarget[5], matrixSourceToTarget[6], matrixSourceToTarget[7], scale };

			var matrixTargetToSource = DenseMatrix.OfArray(new double[,]
			{
				{ result[0], result[1], result[2] },
				{ result[3], result[4], result[5] },
				{ result[6], result[7], scale },
			}).Inverse();

			Console.WriteLine($"Verify x1 = {( matrixTargetToSource[0, 0] * u1 + matrixTargetToSource[0, 1] * v1 + matrixTargetToSource[0, 2] ) / ( matrixTargetToSource[2, 0] * u1 + matrixTargetToSource[2, 1] * v1 + matrixTargetToSource[2, 2] * 1 )}");

			Console.WriteLine($"Verify y1 = {( matrixTargetToSource[1, 0] * u1 + matrixTargetToSource[1, 1] * v1 + matrixTargetToSource[1, 2] ) / ( matrixTargetToSource[2, 0] * u1 + matrixTargetToSource[2, 1] * v1 + matrixTargetToSource[2, 2] * 1 )}");

			Console.ReadLine();
		}
	}
}

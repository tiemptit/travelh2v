using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RecommenderSystem.Core.Helper;

namespace RecommenderSystem.Core.RS_Core
{
    public class MatrixFactorization
    {
        private static int steps = 5000;
        private static double alpha = 0.0002;
        private static double beta = 0.02;
        public static void testc(Matrix R,  Matrix P,  Matrix Q, int K)
        { 
            MatrixFactorize(R, ref P, ref Q, K);
        }
        public static Matrix MatrixFactorize(Matrix R, ref Matrix P, ref Matrix Q, int K)
        {
            Q = Matrix.Transpose(Q);

            double error = 0;

            for (int s = 0; s < steps; s++)
            {
                for (int i = 0; i < R.rows; i++)
                { 
                    for (int j = 0; j < R.cols; j++)
                        if (R[i, j] > 0)
                        {
                            double temp = 0;
                            for (int k = 0; k < K; k++)
                            {
                                temp += P[i, k] * Q[k, j];
                            }
                            double eij = R[i, j] - temp;
                            for (int k = 0; k < K; k++)
                            {
                                P[i, k] = P[i, k] + alpha * (2 * eij * Q[k, j] - beta * P[i, k]);
                                Q[k, j] = Q[k, j] + alpha * (2 * eij * P[i, k] - beta * Q[k, j]);
                            }
                        }
                }

                
                double e = 0;
                for (int i = 0; i < R.rows; i++)
                {
                    for (int j = 0; j < R.cols; j++)
                        if (R[i, j] > 0)
                        {
                            double temp = 0;
                            for (int k = 0; k < K; k++)
                            {
                                temp += P[i, k] * Q[k, j];
                            }
                            
                            e = e + Math.Pow(R[i, j] - temp, 2);
                            for (int k = 0; k < K; k++)
                                e = e + (beta / 2) * (Math.Pow(P[i, k], 2) + Math.Pow(Q[k, j], 2));
                        }
                }
                error = e;
                if (e < 1)
                    break;
            }
            return P * Q;
        }
    }
}

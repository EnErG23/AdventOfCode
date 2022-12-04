using AdventOfCode.Models;

namespace AdventOfCode.Y2021.Days
{
    public class Day24 : Day
    {
        public Day24(int year, int day, bool test) : base(year, day, test) { }

        public override string RunPart1()
        {
            //SerialNumber: 99999738948564 => Z: 8021
            //SerialNumber: 99999738123064 => Z: 8021
            //SerialNumber: 99999546417472 => Z: 8018
            //SerialNumber: 99999546399662 => Z: 8018
            //SerialNumber: 99999514888520 => Z: 8017
            //SerialNumber: 99999514896320 => Z: 8017
            //SerialNumber: 99999514896242 => Z: 8017
            //SerialNumber: 99999514896164 => Z: 8017
            //SerialNumber: 99999514896086 => Z: 8017
            //SerialNumber: 99999909369665 => Z: 8014
            for (long i = 99998717079118/*99999999999999*/; i > 9999999999999; i -= 26)
            {
                Console.WriteLine("--------------------");
                Console.WriteLine($"SerialNumber: {i}");

                if (ValidateSerialNumberCalc(i))
                    return i.ToString();
            }

            return "Undefined";
        }

        public override string RunPart2()
        {
            //long w = 0;
            //long x = 0;
            //long y = 0;
            //long z = 0;

            //return z.ToString();

            return "Undefined";
        }

        public bool ValidateSerialNumber(long serialNumber)
        {
            List<int> inputs = serialNumber.ToString().ToList().Select(s => int.Parse(s.ToString())).ToList();
            var i = 0;

            long w = 0;
            long x = 0;
            long y = 0;
            long z = 0;

            foreach (string input in Inputs)
            {
                string command = input.Split(" ")[0];
                string a = input.Split(" ")[1];
                string bString = command != "inp" ? input.Split(" ")[2] : "0";

                long b = 0;

                switch (bString)
                {
                    case "w":
                        b = w;
                        break;
                    case "x":
                        b = x;
                        break;
                    case "y":
                        b = y;
                        break;
                    case "z":
                        b = z;
                        break;
                    default:
                        b = int.Parse(bString);
                        break;
                }

                switch (command)
                {
                    // inp a - Read an input value and write it to variable a.
                    case "inp":
                        switch (a)
                        {
                            case "w":
                                w = inputs[i++];
                                break;
                            case "x":
                                x = inputs[i++];
                                break;
                            case "y":
                                y = inputs[i++];
                                break;
                            case "z":
                                z = inputs[i++];
                                break;
                            default:
                                break;
                        }
                        break;
                    // add a b - Add the value of a to the value of b, then store the result in variable a.
                    case "add":
                        switch (a)
                        {
                            case "w":
                                w += b;
                                break;
                            case "x":
                                x += b;
                                break;
                            case "y":
                                y += b;
                                break;
                            case "z":
                                z += b;
                                break;
                            default:
                                break;
                        }
                        break;
                    // mul a b - Multiply the value of a by the value of b, then store the result in variable a.
                    case "mul":
                        switch (a)
                        {
                            case "w":
                                w *= b;
                                break;
                            case "x":
                                x *= b;
                                break;
                            case "y":
                                y *= b;
                                break;
                            case "z":
                                z *= b;
                                break;
                            default:
                                break;
                        }
                        break;
                    // div a b - Divide the value of a by the value of b, truncate the result to an integer, then store the result in variable a. (Here, "truncate" means to round the value toward zero.)
                    case "div":
                        switch (a)
                        {
                            case "w":
                                w = (long)Math.Floor(decimal.Parse(w.ToString()) / decimal.Parse(b.ToString()));
                                break;
                            case "x":
                                x = (long)Math.Floor(decimal.Parse(x.ToString()) / decimal.Parse(b.ToString()));
                                break;
                            case "y":
                                y = (long)Math.Floor(decimal.Parse(y.ToString()) / decimal.Parse(b.ToString()));
                                break;
                            case "z":
                                z = (long)Math.Floor(decimal.Parse(z.ToString()) / decimal.Parse(b.ToString()));
                                break;
                            default:
                                break;
                        }
                        break;
                    // mod a b - Divide the value of a by the value of b, then store the remainder in variable a. (This is also called the modulo operation.)
                    case "mod":
                        switch (a)
                        {
                            case "w":
                                w = w % b;
                                break;
                            case "x":
                                x = x % b;
                                break;
                            case "y":
                                y = y % b;
                                break;
                            case "z":
                                z = z % b;
                                break;
                            default:
                                break;
                        }
                        break;
                    // eql a b - If the value of a and b are equal, then store the value 1 in variable a. Otherwise, store the value 0 in variable a.      
                    case "eql":
                        switch (a)
                        {
                            case "w":
                                w = w == b ? 1 : 0;
                                break;
                            case "x":
                                x = x == b ? 1 : 0;
                                break;
                            case "y":
                                y = y == b ? 1 : 0;
                                break;
                            case "z":
                                z = z == b ? 1 : 0;
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }

            Console.WriteLine(z);

            return z == 0;
        }

        public bool ValidateSerialNumberCalc(long serialNumber)
        {
            List<int> digits = serialNumber.ToString().ToList().Select(s => int.Parse(s.ToString())).ToList();
            List<int> b1 = new List<int> { 1, 1, 1, 26, 1, 26, 26, 1, 1, 1, 26, 26, 26, 26 };
            List<int> b2 = new List<int> { 10, 14, 14, -13, 10, -13, -7, 11, 10, 13, -4, -9, -13, -9 };
            List<int> b3 = new List<int> { 2, 13, 13, 9, 15, 3, 6, 5, 16, 1, 6, 3, 7, 9 };

            long z = 0;

            for (int i = 0; i < digits.Count; i++)
            {
                long x = ((z % 26) + b2[i]) != digits[i] ? 1 : 0;
                z = (long)(Math.Floor((decimal)(z / b1[i])) * ((25 * x) + 1)) + ((digits[i] + b3[i]) * x);

                Console.WriteLine("--------------------");
                Console.WriteLine($"w = {digits[i]}"); // w = i
                Console.WriteLine($"x = {x}"); // x = ((z % 26) + b14) != i
                Console.WriteLine($"y = {digits[i] + b3[i]}"); // y = i + b29
                Console.WriteLine($"z = {z}"); // z = ((z/b13) * ((25 * x) + 1)) + (y * x)
            }

            Console.WriteLine("--------------------");
            Console.WriteLine();

            return z == 0;
        }
    }
}
using AdventOfCode.Models;

namespace AdventOfCode.Y2021.Days
{
    public class Day17 : Day
    {
        public Day17(int year, int day, bool test) : base(year, day, test) { }

        public override string RunPart1()
        {
            int result = 0;

            var ranges = Inputs[0].Replace("target area: x=", "").Replace(" y=", "").Replace("..", ",").Split(",");

            int xMin = int.Parse(ranges[0]);
            int xMax = int.Parse(ranges[1]);
            int yMin = int.Parse(ranges[2]);
            int yMax = int.Parse(ranges[3]);

            for (int i = 0; i <= Math.Abs(xMax); i++)
                for (int j = 0; j <= Math.Abs(yMin); j++)
                {
                    int xPos = 0;
                    int yPos = 0;

                    int xVel = i;
                    int yVel = j;

                    int maxYPos = 0;

                    while (true)
                    {
                        int prevXPos = xPos + 0;
                        int prevYPos = yPos + 0;

                        //The probe's x position increases by its x velocity.
                        xPos += xVel;

                        //The probe's y position increases by its y velocity.
                        yPos += yVel;

                        maxYPos = yPos > maxYPos ? yPos : maxYPos;

                        //Due to drag, the probe's x velocity changes by 1 toward the value 0; that is, it decreases by 1 if it is greater than 0, increases by 1 if it is less than 0, or does not change if it is already 0.
                        if (xVel > 0)
                            xVel--;
                        else if (xVel < 0)
                            xVel++;

                        //Due to gravity, the probe's y velocity decreases by 1.
                        yVel--;

                        if (xPos >= xMin && xPos <= xMax && yPos >= yMin && yPos <= yMax)
                        {
                            result = maxYPos > result ? maxYPos : result;
                            break;
                        }
                        else if (xPos > xMax || yPos < yMin || (prevXPos == xPos && prevYPos == yPos && xVel != 0 && yVel != 0))
                        {
                            break;
                        }
                    }
                }

            return result.ToString();
        }

        public override string RunPart2()
        {
            int result = 0;

            var ranges = Inputs[0].Replace("target area: x=", "").Replace(" y=", "").Replace("..", ",").Split(",");

            int xMin = int.Parse(ranges[0]);
            int xMax = int.Parse(ranges[1]);
            int yMin = int.Parse(ranges[2]);
            int yMax = int.Parse(ranges[3]);

            for (int i = 0; i <= Math.Abs(xMax); i++)
                for (int j = -Math.Abs(yMin); j <= Math.Abs(yMin); j++)
                {
                    int xPos = 0;
                    int yPos = 0;

                    int xVel = i;
                    int yVel = j;

                    while (true)
                    {
                        int prevXPos = xPos + 0;
                        int prevYPos = yPos + 0;

                        //The probe's x position increases by its x velocity.
                        xPos += xVel;

                        //The probe's y position increases by its y velocity.
                        yPos += yVel;

                        //Due to drag, the probe's x velocity changes by 1 toward the value 0; that is, it decreases by 1 if it is greater than 0, increases by 1 if it is less than 0, or does not change if it is already 0.
                        if (xVel > 0)
                            xVel--;
                        else if (xVel < 0)
                            xVel++;

                        //Due to gravity, the probe's y velocity decreases by 1.
                        yVel--;

                        if (xPos >= xMin && xPos <= xMax && yPos >= yMin && yPos <= yMax)
                        {
                            result++;
                            break;
                        }
                        else if (xPos > xMax || yPos < yMin || (prevXPos == xPos && prevYPos == yPos && xVel != 0 && yVel != 0))
                        {
                            break;
                        }
                    }
                }

            return result.ToString();
        }
    }
}
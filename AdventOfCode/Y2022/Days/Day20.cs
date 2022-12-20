using AdventOfCode.Models;

namespace AdventOfCode.Y2022.Days
{
    public class Day20 : Day
    {
        private List<Number> _numbers;

        public Day20(int year, int day, bool test) : base(year, day, test)
            => _numbers = Enumerable.Range(0, Inputs.Count).Select(e => new Number(e, long.Parse(Inputs[e]))).ToList();

        public override string RunPart1()
        {
            MixNumbers();

            long amountOfNumbers = _numbers.Count;

            long positionOfZero = _numbers.FirstOrDefault(c => c.Value == 0).Position;

            long positionOfFirstNumber = (positionOfZero + 1000) % amountOfNumbers;
            long positionOfSecondNumber = (positionOfZero + 2000) % amountOfNumbers;
            long positionOfThirdNumber = (positionOfZero + 3000) % amountOfNumbers;

            return (_numbers.FirstOrDefault(c => c.Position == positionOfFirstNumber).Value
                + _numbers.FirstOrDefault(c => c.Position == positionOfSecondNumber).Value
                + _numbers.FirstOrDefault(c => c.Position == positionOfThirdNumber).Value)
                .ToString();
        }

        public override string RunPart2()
        {
            _numbers = Enumerable.Range(0, Inputs.Count).Select(e => new Number(e, long.Parse(Inputs[e]) * 811589153)).ToList();

            for (int i = 0; i < 10; i++)
                MixNumbers();

            long amountOfNumbers = _numbers.Count;

            long positionOfZero = _numbers.FirstOrDefault(c => c.Value == 0).Position;

            long positionOfFirstNumber = (positionOfZero + 1000) % amountOfNumbers;
            long positionOfSecondNumber = (positionOfZero + 2000) % amountOfNumbers;
            long positionOfThirdNumber = (positionOfZero + 3000) % amountOfNumbers;

            return (_numbers.FirstOrDefault(c => c.Position == positionOfFirstNumber).Value
                + _numbers.FirstOrDefault(c => c.Position == positionOfSecondNumber).Value
                + _numbers.FirstOrDefault(c => c.Position == positionOfThirdNumber).Value)
                .ToString();
        }

        public void MixNumbers()
        {
            for (int i = 0; i < _numbers.Count; i++)
            {
                Number number = _numbers[i];
                long value = number.Value;

                if (value == 0)
                    continue;

                long currentPos = number.Position;
                long amountOfNumbers = _numbers.Count;

                // Substract one from amount of numbers for new position calculation (can't jump over itself, so one number less in list)                
                long possibleNewPos = value > 0 ?
                    currentPos + (value % (amountOfNumbers - 1))
                    : currentPos - (Math.Abs(value) % (amountOfNumbers - 1));

                // Calculate actual new position when calculated position falls outside of range [0..AmountOfNumbers]
                long newPos = value > 0 ?
                    (possibleNewPos >= amountOfNumbers ?
                        possibleNewPos - amountOfNumbers + 1
                        : possibleNewPos)
                    : (possibleNewPos <= 0 ?
                        possibleNewPos + amountOfNumbers - 1
                        : possibleNewPos);

                // Shift positions of affected numbers before or after
                if ((value > 0 && possibleNewPos < amountOfNumbers) || (value < 0 && possibleNewPos <= 0))
                    _numbers.Where(c => c.Position > currentPos && c.Position <= newPos).ToList().ForEach(c => c.Position--);
                else
                    _numbers.Where(c => c.Position >= newPos && c.Position < currentPos).ToList().ForEach(c => c.Position++);

                // Update number position
                number.Position = newPos;
            }
        }
    }

    public class Number
    {
        public long Position { get; set; }
        public long Value { get; set; }

        public Number(long pos, long val)
        {
            Position = pos;
            Value = val;
        }
    }
}
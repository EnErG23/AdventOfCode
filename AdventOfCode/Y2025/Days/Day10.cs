using AdventOfCode.Models;
using AdventOfCode.Helpers;
using System.Net;

namespace AdventOfCode.Y2025.Days
{
    public class Day10 : Day
    {
        private List<State> _initialStates;
        private Queue<State> _states;

        public Day10(int year, int day, bool test) : base(year, day, test)
        {
            _initialStates = new();
            _states = new();

            foreach (var input in Inputs)
            {
                var indicatorTargets = input
                    .Split(" ")[0]
                    .Replace("[", "")
                    .Replace("]", "")
                    .Select(i => i == '#')
                    .ToList();

                var indicators = input
                    .Split(" ")[0]
                    .Replace("[", "")
                    .Replace("]", "")
                    .Select(i => false)
                    .ToList();

                var testButtons = input
                    .Split("]")[1]
                    .Split("{")[0]
                    .Replace("(", "")
                    .Replace(")", "")
                    .Trim()
                    .Split(" ");

                var buttons = testButtons
                    .Select(i => i
                        .Split(",")
                        .Select(n => int.Parse(n))
                        .ToList())
                    .ToList();

                var joltTargets = input
                    .Split("{")[1]
                    .Replace("}", "")
                    .Split(",")
                    .Select(i => int.Parse(i))
                    .ToList();

                var jolts = input
                    .Split("{")[1]
                    .Replace("}", "")
                    .Split(",")
                    .Select(i => 0)
                    .ToList();

                _initialStates.Add(new(indicatorTargets, indicators, buttons, joltTargets, jolts, 0));
            }
        }

        public override string RunPart1()
        {
            var result = 0;

            foreach (var initialState in _initialStates)
            {
                _states.Enqueue(initialState);

                while (_states.Any())
                {
                    var state = _states.Dequeue();

                    foreach (var button in state.Buttons)
                    {
                        var newState = PressButton(state, button);

                        if (IsTargetReached(newState))
                        {
                            result += newState.ButtonsPressed;
                            _states.Clear();
                            break;
                        }

                        _states.Enqueue(newState);
                    }
                }
            }

            return result.ToString();
        }

        public override string RunPart2()
        {
            var result = 0;

            foreach (var initialState in _initialStates)
            {
                _states.Enqueue(initialState);

                while (_states.Any())
                {
                    var state = _states.Dequeue();

                    foreach (var button in state.Buttons)
                    {
                        var newState = PressJoltageButton(state, button);

                        if (IsOverJoltageTarget(newState))
                            break;

                        if (IsJoltageTargetReached(newState))
                        {
                            result += newState.ButtonsPressed;
                            _states.Clear();
                            break;
                        }

                        _states.Enqueue(newState);
                    }
                }
            }

            return result.ToString();
        }

        private State PressButton(State state, List<int> buttons)
        {
            var newState = new State(state);

            foreach (var button in buttons)
                newState.Indicators[button] = !newState.Indicators[button];

            newState.ButtonsPressed++;

            return newState;
        }

        private State PressJoltageButton(State state, List<int> buttons)
        {
            var newState = new State(state);

            foreach (var button in buttons)
                newState.Jolts[button]++;

            newState.ButtonsPressed++;

            return newState;
        }

        private bool IsTargetReached(State state)
        {
            for (int i = 0; i < state.Indicators.Count; i++)
            {
                if (state.IndicatorTargets[i] != state.Indicators[i])
                    return false;
            }

            return true;
        }

        private bool IsOverJoltageTarget(State state)
        {
            for (int i = 0; i < state.Indicators.Count; i++)
            {
                if (state.JoltTargets[i] < state.Jolts[i])
                    return true;
            }

            return false;
        }

        private bool IsJoltageTargetReached(State state)
        {
            for (int i = 0; i < state.Indicators.Count; i++)
            {
                if (state.JoltTargets[i] != state.Jolts[i])
                    return false;
            }

            return true;
        }
    }

    public class State
    {
        public List<bool> IndicatorTargets { get; set; }
        public List<bool> Indicators { get; set; }
        public List<List<int>> Buttons { get; set; }
        public List<int> JoltTargets { get; set; }
        public List<int> Jolts { get; set; }
        public int ButtonsPressed { get; set; }

        public State(List<bool> indicatorTargets, List<bool> indicators, List<List<int>> buttons, List<int> joltTargets, List<int> jolts, int buttonsPressed)
        {
            IndicatorTargets = indicatorTargets;
            Indicators = indicators;
            Buttons = buttons;
            Jolts = jolts;
            JoltTargets = joltTargets;
            ButtonsPressed = buttonsPressed;
        }

        public State(State state)
        {
            IndicatorTargets = state.IndicatorTargets.ToList();
            Indicators = state.Indicators.ToList();
            Buttons = state.Buttons.ToList();
            Jolts = state.Jolts.ToList();
            JoltTargets = state.JoltTargets.ToList();
            ButtonsPressed = state.ButtonsPressed;
        }
    }
}

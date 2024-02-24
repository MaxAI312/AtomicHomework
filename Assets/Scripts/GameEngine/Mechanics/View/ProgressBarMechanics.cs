using System;
using GameEngine;

namespace Content
{
    [Serializable]
    public class ProgressBarMechanics
    {
        private readonly ProgressBar _bar;
        private readonly Countdown _countdown;

        public ProgressBarMechanics(ProgressBar bar, Countdown countdown)
        {
            _bar = bar;
            _countdown = countdown;
        }

        public void Update()
        {
            float progress = 1 - (_countdown.RemainingTime / _countdown.Duration);
            _bar.SetProgress(progress);
        }
    }
}
// Copyright(c) Cragon. All rights reserved.

namespace Casinos
{
    using System;
    using FairyGUI;

    public class UISlideEx
    {
        //---------------------------------------------------------------------
        public long CurrentValueNum { get; private set; }
        GSlider UISlider { get; set; }
        Action<long> SliderChangeAction { get; set; }
        long TopNum { get; set; }
        long BottomNum { get; set; }

        //---------------------------------------------------------------------
        public UISlideEx(GSlider slider, long top_num, long bottom_num,
            float slider_defaultvalue, Action<long> slider_changeaction)
        {
            UISlider = slider;
            UISlider.max = 1;
            TopNum = top_num;
            BottomNum = bottom_num;
            UISlider.value = slider_defaultvalue;
            CurrentValueNum = (long)((TopNum + BottomNum) * UISlider.value);
            UISlider.onChanged.Add(_sliderChange);
            SliderChangeAction = slider_changeaction;
        }

        //---------------------------------------------------------------------
        public void changeValue(bool is_plus, float change_value)
        {
            var value = UISlider.value;
            if (is_plus)
            {
                value += change_value;
            }
            else
            {
                value -= change_value;
            }

            if (value > 1)
            {
                value = 1;
            }

            if (value < 0)
            {
                value = 0;
            }

            UISlider.value = value;
            _sliderChange();
        }

        //---------------------------------------------------------------------
        void _sliderChange()
        {
            long value = (long)((TopNum - BottomNum) * UISlider.value);
            value += BottomNum;
            CurrentValueNum = value;
            if (SliderChangeAction != null)
            {
                SliderChangeAction(CurrentValueNum);
            }
        }
    }
}
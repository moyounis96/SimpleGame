﻿#define RTL_OVERRIDE

using System;
using System.Linq;
using TMPro;
using UnityEngine;

namespace RTLTMPro
{
    [ExecuteInEditMode]
    public class RTLTextMeshPro : TextMeshProUGUI
    {
        // ReSharper disable once InconsistentNaming
#if RTL_OVERRIDE
        public override string text
#else
        public new string text
#endif
        {
            get { return base.text; }
            set
            {
                if (originalText == value)
                    return;
                translationKey = value;
                originalText = value;
                UpdateText();
            }
        }

        public string OriginalText
        {
            get { return originalText; }
        }

        public bool PreserveNumbers
        {
            get { return preserveNumbers; }
            set
            {
                if (preserveNumbers == value)
                    return;

                preserveNumbers = value;
                havePropertiesChanged = true;
            }
        }

        public bool Farsi
        {
            get { return farsi; }
            set
            {
                if (farsi == value)
                    return;

                farsi = value;
                havePropertiesChanged = true;
            }
        }

        public bool FixTags
        {
            get { return fixTags; }
            set
            {
                if (fixTags == value)
                    return;

                fixTags = value;
                havePropertiesChanged = true;
            }
        }

        protected bool ForceFix
        {
            get { return forceFix; }
            set
            {
                if (forceFix == value)
                    return;

                forceFix = value;
                havePropertiesChanged = true;
            }
        }

        [SerializeField]
        protected bool preserveNumbers = true;

        [SerializeField]
        protected bool farsi = true;

        [SerializeField]
        [TextArea(3, 10)]
        protected string originalText;

        [SerializeField]
        protected bool fixTags = true;

        [SerializeField]
        protected bool forceFix;

        protected void Update()
        {
            if (havePropertiesChanged)
            {
                UpdateText();
            }
        }

        public void UpdateText()
        {
            if (originalText == null)
                originalText = "";

            if (ForceFix == false && RTLSupport.IsRTLInput(originalText) == false)
            {
                isRightToLeftText = false;
                base.text = originalText;
            }
            else
            {
                isRightToLeftText = true;
                base.text = GetFixedText(originalText);
            }

            havePropertiesChanged = true;
        }

        private string GetFixedText(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            input = RTLSupport.FixRTL(input, fixTags, preserveNumbers, farsi);
            input = input.Reverse().ToArray().ArrayToString();

            return input;
        }

        string translationKey = "";
        public void UpdateTranslation () {
            if (translationKey == "")
                translationKey = originalText;
            originalText = LanguageManager.GetString (translationKey);
            UpdateText ();
        }
    }
}
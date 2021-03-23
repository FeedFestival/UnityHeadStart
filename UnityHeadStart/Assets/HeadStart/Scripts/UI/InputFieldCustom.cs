using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldCustom : MonoBehaviour
{
    public Image BorderDown;
    public Text Label;
    public InputField InputField;

    public bool AllUppercase;

    private float _animationSpeed = 0.2f;

    private float _labelWidth;
    private float _labelMaxHeight;
    private float _labelMinHeight = 40f;
    private int _labelFontSize;
    private int _labelMinFontSize = 20;

    private float _borderDownHeight;
    private float _borderDownMaxWidth = 340f;
    private float _borderDownMinWidth = 0f;

    public delegate void OnBlurCallback();
    public OnBlurCallback OnBlurDelegate;

    public delegate void OnChangeCallback();
    public OnChangeCallback OnChangeDelegate;

    private void Start()
    {
        Init();
        OnBlur();
    }

    private void Init()
    {
        _labelMaxHeight = Label.GetComponent<RectTransform>().sizeDelta.y;
        _labelWidth = Label.GetComponent<RectTransform>().sizeDelta.x;
        _labelFontSize = Label.fontSize;

        _borderDownHeight = BorderDown.GetComponent<RectTransform>().sizeDelta.y;
    }

    public void OnFocus()
    {
        if (string.IsNullOrWhiteSpace(InputField.text))
        {
            LeanTween.value(Label.gameObject, (float value) =>
            {
                Label.GetComponent<RectTransform>().sizeDelta = new Vector2(_labelWidth, value);
            }, _labelMaxHeight, _labelMinHeight, _animationSpeed);
            LeanTween.value(Label.gameObject, (float value) =>
            {
                Label.fontSize = (int)value;
            }, _labelFontSize, _labelMinFontSize, _animationSpeed);
        }
        else
        {
            var color = ColorBank._.Red_Brown_Eggplant;
            color.a = 0;
            Label.color = color;
            LeanTween.alphaText(Label.gameObject.GetComponent<RectTransform>(), 1, _animationSpeed);
        }

        LeanTween.value(BorderDown.gameObject, (float value) =>
        {
            BorderDown.GetComponent<RectTransform>().sizeDelta = new Vector3(value, _borderDownHeight);
        }, _borderDownMinWidth, _borderDownMaxWidth, _animationSpeed);
    }

    public void OnBlur(bool initial = false)
    {
        if (initial)
        {
            Init();

            if (string.IsNullOrWhiteSpace(InputField.text) == false)
            {
                Label.GetComponent<RectTransform>().sizeDelta = new Vector2(_labelWidth, _labelMinHeight);
                Label.fontSize = _labelMinFontSize;
                BorderDown.GetComponent<RectTransform>().sizeDelta = new Vector3(_borderDownMinWidth, _borderDownHeight);
            }

            return;
        }

        if (string.IsNullOrWhiteSpace(InputField.text))
        {
            LeanTween.value(Label.gameObject, (float value) =>
            {
                Label.GetComponent<RectTransform>().sizeDelta = new Vector2(_labelWidth, value);
            }, _labelMinHeight, _labelMaxHeight, _animationSpeed);
            LeanTween.value(Label.gameObject, (float value) =>
            {
                Label.fontSize = (int)value;
            }, _labelMinFontSize, _labelFontSize, _animationSpeed);
        }
        else
        {
            var color = ColorBank._.Red_Brown_Eggplant;
            color.a = 255;
            Label.color = color;
            LeanTween.alphaText(Label.gameObject.GetComponent<RectTransform>(), 0, _animationSpeed);
        }

        LeanTween.value(BorderDown.gameObject, (float value) =>
        {
            BorderDown.GetComponent<RectTransform>().sizeDelta = new Vector3(value, _borderDownHeight);
        }, _borderDownMaxWidth, _borderDownMinWidth, _animationSpeed);

        OnBlurDelegate?.Invoke();
    }

    public void OnChange()
    {
        if (AllUppercase)
            InputField.text = InputField.text.ToUpper();

        OnChangeDelegate?.Invoke();
    }
}

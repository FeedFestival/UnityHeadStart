using DentedPixel;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldCustom : MonoBehaviour
{
#pragma warning disable 0414 // private field assigned but not used.
    public static readonly string _version = "2.2.0";
#pragma warning restore 0414 //
    public Image BorderDown;
    public Text Label;
    public InputField InputField;
    public Color LabelColor;

    public bool AllUppercase;

    private float _animationSpeed = 0.2f;

    private float _labelWidth;
    private float _labelMaxHeight;
    private float _labelMinHeight = 40f;
    private int _labelFontSize;
    private int _labelMinFontSize = 20;
    [SerializeField]
    private float _borderDownHeight;
    [SerializeField]
    private float _borderDownMaxWidth = 340f;
    [SerializeField]
    private float _borderDownMinWidth = 0f;
    private RectTransform _rt;

    public delegate void OnBlurCallback();
    public OnBlurCallback OnBlurDelegate;

    public delegate void OnChangeCallback();
    public OnChangeCallback OnChangeDelegate;

    public void Init()
    {
        _rt = transform as RectTransform;
        _labelWidth = _rt.sizeDelta.x;
        setLabelColor(1);
        InternalInit();
        OnBlur();
    }

    private void InternalInit()
    {
        _labelMaxHeight = Label.GetComponent<RectTransform>().sizeDelta.y;
        Label.GetComponent<RectTransform>().sizeDelta = new Vector2(_labelWidth, _labelMaxHeight);
        _labelFontSize = Label.fontSize;

        _borderDownHeight = BorderDown.GetComponent<RectTransform>().sizeDelta.y;
        _borderDownMaxWidth = _labelWidth;
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
            setLabelColor();
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
            InternalInit();

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
            setLabelColor(1);
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

    private void setLabelColor(float a = 0)
    {
        var color = LabelColor;
        color.a = a;
        Label.color = color;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ExtendedDropdown : MonoBehaviour
{
    [SerializeField]
    private Dropdown _dropdown;

    [SerializeField]
    private string _defaultTextOption;

    private List<string> _options;
    private string _selectedOption;

    public void Init()
    {
        _options = new List<string>();
        UpdateDropdownOptions(_options);
    }

    public void UpdateDropdownOptions(List<string> options)
    {
        _dropdown.ClearOptions();
        _dropdown.AddOptions(CreateOptionDatas(options));
        _dropdown.RefreshShownValue();

        SetSelectedOption();
    }

    private List<Dropdown.OptionData> CreateOptionDatas(List<string> options)
    {
        List<Dropdown.OptionData> optionDatas = new List<Dropdown.OptionData>();
        _options = options;

        optionDatas.Add(new Dropdown.OptionData(_defaultTextOption));
        if (_options != null)
        {
            foreach (string option in _options)
            {
                optionDatas.Add(new Dropdown.OptionData(option));
            }
        }

        return optionDatas;
    }

    private void SetSelectedOption()
    {
        if (_selectedOption != null)
        {
            List<Dropdown.OptionData> options = _dropdown.options.ToList();
            foreach (Dropdown.OptionData option in options)
            {
                if (option.text == _selectedOption)
                {
                    _dropdown.SetValueWithoutNotify(options.IndexOf(option));
                    return;
                }
            }
        }

        _dropdown.SetValueWithoutNotify(0);
    }

    private string GetOption()
    {
        if (_dropdown.value == 0)
        {
            return null;
        }
        return _dropdown.captionText.text;
    }

    public string GetSelectedOption()
    {
        return _selectedOption;
    }

    public void ChangeSelectedOption()
    {
        _selectedOption = GetOption();
        SelectOption?.Invoke(_selectedOption);
    }

    public event Action<string> SelectOption;
}
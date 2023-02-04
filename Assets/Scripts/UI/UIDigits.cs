using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteInEditMode]
public class UIDigits : MonoBehaviour {

    public float value;

    public TextMeshProUGUI[] digits;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

        char[] digitChars = Mathf.RoundToInt(value).ToString().PadLeft(digits.Length, ' ').ToCharArray();

        for (int i = 0; i < digits.Length; i++) {
            digits[i].text = digitChars[i].ToString();
            digits[i].gameObject.SetActive(digitChars[i] != ' ');
        }

    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OverlapSensor : MonoBehaviour
{
    [SerializeField] Image piggyBankSpritePlaceholder;
    [SerializeField] Sprite[] piggyBankSprites;
    [SerializeField] TextMeshProUGUI amountInputtedText;
    [SerializeField] Button resetButton;
    [SerializeField] AudioClip moneyEnteredSoundEffect;
    [SerializeField] AudioClip resetSoundEffect;
    private int dollarsInPiggyBank = 0;
    private int centsInPiggyBank = 0;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        UpdateAmountInputtedText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Money"))
        {
            piggyBankSpritePlaceholder.sprite = piggyBankSprites[1];
        }
    }

    void OnTriggerExit(Collider other)
    {
        piggyBankSpritePlaceholder.sprite = piggyBankSprites[0];
        if (other.CompareTag("Money") && !other.gameObject.GetComponent<BillAndCoinBehavior>().GetMouseDown())
        {
            audioSource.PlayOneShot(moneyEnteredSoundEffect, 1);
            dollarsInPiggyBank += other.gameObject.GetComponent<BillAndCoinBehavior>().GetDollars();
            centsInPiggyBank += other.gameObject.GetComponent<BillAndCoinBehavior>().GetCents();
            UpdateAmountInputtedText();
        }
    }

    private void UpdateAmountInputtedText()
    {
        dollarsInPiggyBank += centsInPiggyBank / 100;
        centsInPiggyBank %= 100;
        if (centsInPiggyBank >= 10)
        {
            amountInputtedText.text = "Amount Inputted: $" + dollarsInPiggyBank.ToString() + "." + centsInPiggyBank.ToString();
        }
        else
        {
            amountInputtedText.text = "Amount Inputted: $" + dollarsInPiggyBank.ToString() + ".0" + centsInPiggyBank.ToString();
        }
    }

    public void ResetAmountInputted(bool withAudioClip)
    {
        dollarsInPiggyBank = 0;
        centsInPiggyBank = 0;
        if (withAudioClip)
        {
            audioSource.PlayOneShot(resetSoundEffect, 1);
        }
        UpdateAmountInputtedText();
        resetButton.enabled = false;
        resetButton.enabled = true;
    }

    public int GetDollarsInPiggyBank()
    {
        return dollarsInPiggyBank;
    }

    public int GetCentsInPiggyBank()
    {
        return centsInPiggyBank;
    }
}

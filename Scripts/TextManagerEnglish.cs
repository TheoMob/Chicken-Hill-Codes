using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextManagerEnglish : MonoBehaviour
{
    [SerializeField] private Sprite[] characterPortraits;
    [SerializeField] private Image activePortrait;
    [SerializeField] private TextMeshProUGUI nameText;
    private Animator portraitAnim;

    public DialogScriptableObject[] textDataEnglish;
    public GameObject textBox;
    private SoundManager soundManagerScript;
    private Text textBoxText;
    private int textIndexer = 0;
    private int textPage = 0;
    public float delayTextTime = 0.08f;
    private bool writingText = false;
    private bool dialogProgressionLocal;

    private void Start()
    {
        soundManagerScript = gameObject.GetComponent<SoundManager>();
        textBox = GameObject.FindGameObjectWithTag("TextBox");
        textBoxText = textBox.transform.GetChild(0).GetComponent<Text>();
        portraitAnim = textBox.transform.GetChild(2).gameObject.GetComponent<Animator>();
        nameText = textBox.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        activePortrait = textBox.transform.GetChild(2).GetComponent<Image>();
        //textBox.SetActive(false);
        resetDialogs();

        DontDestroyOnLoad(gameObject);
    }

    private void resetDialogs() // isso serve apenas pra testes enquanto o jogo n�o ta pronto
    {
        for (int i = 0; i < textDataEnglish.Length; i++)
        {
            textDataEnglish[i].dialogActive = 0;
        }
    }
    public void TextScroller(int textIndex, bool dialogProgression)
    {
        if (textIndex != 0) // textIndex = 0 serve pra representar que o player n�o clicou em um objeto interagivel
        {
            textIndexer = textIndex;
            dialogProgressionLocal = dialogProgression;
        }
        if ((textIndex == 0) && (!textBox.activeInHierarchy))
            return;


        textBox.SetActive(true);
        portraitChanger();
        if (textDataEnglish[textIndexer].dialogActive == 0)
        {
            if (textPage < textDataEnglish[textIndexer].DialogAmount.Count)
            {
                if ((writingText == false))
                {
                    writingText = true;
                    soundManagerScript.playTypingSound(true);
                    StartCoroutine("TypeWriterEffect");
                }
                else
                {
                    textBoxText.text = textDataEnglish[textIndexer].DialogAmount[textPage].DialogBox;
                    writingText = false;
                    soundManagerScript.playTypingSound(false);
                }
            }
            else
            {
                textPage = 0;
                if (dialogProgressionLocal && textDataEnglish[textIndexer].dialogActive < 3)
                    textDataEnglish[textIndexer].dialogActive++;
                textBox.SetActive(false);
            }
        }
        else
        {
            if (textDataEnglish[textIndexer].dialogActive == 1)
            {
                if (textPage < textDataEnglish[textIndexer].DialogAmount2.Count)
                {
                    if ((writingText == false))
                    {
                        writingText = true;
                        soundManagerScript.playTypingSound(true);
                        StartCoroutine("TypeWriterEffect");
                    }
                    else
                    {
                        textBoxText.text = textDataEnglish[textIndexer].DialogAmount2[textPage].DialogBox;
                        writingText = false;
                        soundManagerScript.playTypingSound(false);
                    }
                }
                else
                {
                    textPage = 0;
                    if (dialogProgressionLocal && textDataEnglish[textIndexer].dialogActive < 3)
                        textDataEnglish[textIndexer].dialogActive++;
                    textBox.SetActive(false);
                }
            }
            else if (textPage < textDataEnglish[textIndexer].DialogAmount3.Count)
            {
                if ((writingText == false))
                {
                    writingText = true;
                    soundManagerScript.playTypingSound(true);
                    StartCoroutine("TypeWriterEffect");
                }
                else
                {
                    textBoxText.text = textDataEnglish[textIndexer].DialogAmount3[textPage].DialogBox;
                    writingText = false;
                    soundManagerScript.playTypingSound(false);
                }
            }
            else
            {
                textPage = 0;
                if (dialogProgressionLocal && textDataEnglish[textIndexer].dialogActive < 3)
                    textDataEnglish[textIndexer].dialogActive++;
                textBox.SetActive(false);
            }
        }

    }
    IEnumerator TypeWriterEffect()
    {
        string eventText;

        switch (textDataEnglish[textIndexer].dialogActive)
        {
            case 0:
                eventText = textDataEnglish[textIndexer].DialogAmount[textPage].DialogBox;
                break;
            case 1:
                eventText = textDataEnglish[textIndexer].DialogAmount2[textPage].DialogBox;
                break;
            default:
                eventText = textDataEnglish[textIndexer].DialogAmount3[textPage].DialogBox;
                break;

        }

        for (int i = 0; i < eventText.Length; i++)
        {
            if (writingText == false)
            {
                break;
            }

            textBoxText.text = eventText.Substring(0, i + 1);
            yield return new WaitForSeconds(delayTextTime);
        }
        textPage++;
        writingText = false;
        soundManagerScript.playTypingSound(false);
    }

    private void portraitChanger()
    {
        List<int> portraitIndex = new List<int>();
        int actualIndex;

        if (textDataEnglish[textIndexer].dialogActive == 0)
            portraitIndex = textDataEnglish[textIndexer].characterIndex0;
        else
        {
            if (textDataEnglish[textIndexer].dialogActive == 1)
                portraitIndex = textDataEnglish[textIndexer].characterIndex1;
            else
                portraitIndex = textDataEnglish[textIndexer].characterIndex2;
        }

        if (textPage < portraitIndex.Count)
            actualIndex = portraitIndex[textPage];
        else
            actualIndex = 0;

        //activePortrait.sprite = characterPortraits[actualIndex];

        switch (actualIndex)
        {
            case 0:
                nameText.text = "Digueliro";
                portraitAnim.SetTrigger("Dig_Trig");
                break;
            case 1:
                nameText.text = "Bartolomeu";
                portraitAnim.SetTrigger("Bart_Trig");
                break;
            case 2:
                nameText.text = "Rubens";
                portraitAnim.SetTrigger("Rub_Trig");
                break;
            case 5:
                nameText.text = "Gazela";
                portraitAnim.SetTrigger("Gaz_Trig");
                break;
        }
    }
}

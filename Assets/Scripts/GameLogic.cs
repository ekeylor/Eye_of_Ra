using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour {
	private IList<string> glyphValues = new List<string>(new string[] {"A", "a", "i", "w", "y"});
	private const int minMasteryNum = 5;
	private IList<int> currentGlyphCount = new List<int> (new int[]{ 0, 0, 0, 0 });
	private int maxGlyphIndex;
	private int newGlyphNum, oldGlyphNum;
	private string newGlyphEng;
	private List<int> answers = new List<int> ();

	private Text currentGlyphEng;
	private Image leftGlyphImg;
	private Image centerGlyphImg;
	private Image rightGlyphImg;

	public Sprite Aglyph;
	public Sprite asglyph;
	public Sprite iglyph;
	public Sprite wglyph;
	public Sprite yglyph;

	private IDictionary<int, Sprite> hieroglyphSprites;


	// Use this for initialization
	void Start () {
		//Cursor.visible = true;
		//Screen.lockCursor = false;
		maxGlyphIndex = 4;
		oldGlyphNum = newGlyphNum = -1;

		hieroglyphSprites = new Dictionary<int, Sprite>() {
			{0, Aglyph},
			{1, asglyph},
			{2, iglyph},
			{3, wglyph},
			{4, yglyph}
		};

		currentGlyphEng = GameObject.FindGameObjectWithTag ("CurrentGlyphEng").GetComponent<Text> ();
		leftGlyphImg = GameObject.FindGameObjectWithTag ("LeftGlyphImg").GetComponent<Image> ();
		centerGlyphImg = GameObject.FindGameObjectWithTag ("CenterGlyphImg").GetComponent<Image> ();
		rightGlyphImg = GameObject.FindGameObjectWithTag ("RightGlyphImg").GetComponent<Image> ();

		SelectNewGlyph ();
		ResetForNewGlyph ();
	}
	
	// Update is called once per frame
	void Update () {}

	public void SelectNewGlyph() {

		do {
			newGlyphNum = Random.Range (0, maxGlyphIndex);
		} while(oldGlyphNum == newGlyphNum);

		oldGlyphNum = newGlyphNum;
		newGlyphEng = glyphValues [newGlyphNum];
	}

	private void ResetForNewGlyph() {
		// Select other (incorrect) glyphs
		List<int> answersCopy = new List<int> ();
		answersCopy.Add (newGlyphNum);

		do {
			int value = Random.Range(0, maxGlyphIndex);
			if(!answersCopy.Contains(value)) {
				answersCopy.Add(value);
			}
			// Don't repeat correct glyph from previous question. ?
		} while(answersCopy.Count < 3);

		// Assign to left, center, right
		answers.Clear();

		int selection = Random.Range (0, 3);
		answers.Add(answersCopy[selection]);
		answersCopy.RemoveAt(selection);
		leftGlyphImg.sprite = hieroglyphSprites[answers[0]];

		selection = Random.Range (0, 2);
		answers.Add(answersCopy[selection]);
		answersCopy.RemoveAt (selection);
		centerGlyphImg.sprite = hieroglyphSprites[answers[1]];

		answers.Add(answersCopy[0]);
		rightGlyphImg.sprite = hieroglyphSprites[answers[2]];

		// Put English version of target glyph on boat
		currentGlyphEng.text = newGlyphEng;
	}

	private void ProcessAnswer(int answer) {
		if (answer == answers.IndexOf (newGlyphNum))
			Debug.Log ("correct");
		else
			Debug.Log ("incorrect");
	
		// Decide whether to extend number of glyphs available.

		SelectNewGlyph ();
		ResetForNewGlyph ();
	}

	public void InputAnswer(string answer) {
		if (answer.Equals ("Left"))
			ProcessAnswer (0);
		else if (answer.Equals ("Center"))
			ProcessAnswer (1);
		else
			ProcessAnswer (2);
	}
}
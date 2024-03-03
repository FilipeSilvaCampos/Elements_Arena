using DG.Tweening;
using ElementsArena.UI;
using UnityEngine;
using UnityEngine.UI;

public class SelectArenaScreen : Menu
{ 
	[SerializeField] Image uIImage;
	[SerializeField] GameObject menuObject;
	[SerializeField] ArenaScriptableObject[] arenas;

	[Header("Animation Details")]
	[Tooltip("Indicators are to punch with the UIImage")]
	[SerializeField] GameObject rightIndicator;
	[SerializeField] GameObject leftIndicator;
	[SerializeField] float punchDuration = .3f;
	[SerializeField] int punchVibrato = 20;
	[SerializeField] int punchElasticity = 10;

	int selectedArena = 0;
	bool isShowed = false;

	float timer = 0; //Lazy solution to prevent player skip the select arena screen

	private void Update()
	{
		timer += Time.deltaTime;

		if (!isShowed) return;

		if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) ShowNextArena();

		if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) ShowPreviousArena();

		if (Input.GetKeyDown(KeyCode.Return) && timer > .5f)
			GetComponent<MenuManager>().gameManager.LoadGame(arenas[selectedArena].buildIndex);
	}

	void ShowPreviousArena()
	{
		PunchAnim(leftIndicator);

		if (selectedArena == 0) return;

		selectedArena--;
		PunchAnim(uIImage.gameObject);
		uIImage.sprite = arenas[selectedArena].previewImage;
	}

	void ShowNextArena()
	{
		PunchAnim(rightIndicator);

		if (selectedArena >= arenas.Length - 1) return;

		selectedArena++;
		PunchAnim(uIImage.gameObject);
		uIImage.sprite = arenas[selectedArena].previewImage;
	}

	void PunchAnim(GameObject toPunch) => toPunch.transform.DOPunchPosition(Vector3.down, punchDuration, punchVibrato, punchElasticity);
	

	public override void Hide()
	{
		GetComponent<MenuManager>().gameManager.ResetGame();
		menuObject.SetActive(false);
		isShowed = false;
	}

	public override void Show()
	{
		menuObject.SetActive(true);
		selectedArena = 0;
		timer = 0;
		uIImage.sprite = arenas[selectedArena].previewImage;
		isShowed = true;
	}
}

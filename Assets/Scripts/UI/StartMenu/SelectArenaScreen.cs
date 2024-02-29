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
		if (selectedArena == 0) return;

		selectedArena--;
		uIImage.transform.DOPunchPosition(Vector3.down, punchDuration, punchVibrato, punchElasticity);
		uIImage.sprite = arenas[selectedArena].previewImage;
	}

	void ShowNextArena()
	{
		if (selectedArena >= arenas.Length - 1) return;

		selectedArena++;
		uIImage.transform.DOPunchPosition(Vector3.down, punchDuration, punchVibrato, punchElasticity);
		uIImage.sprite = arenas[selectedArena].previewImage;
	}

	public override void Hide()
	{
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

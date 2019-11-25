using DG.Tweening;
using UniRx.Async;
using UnityEngine;
using UnityEngine.UI;

public class DOTweenList_Example : MonoBehaviour
{
	public Text message;

	public RectTransform rectA;
	public RectTransform rectB;
	public RectTransform rectC;

	public Button button1;
	public Button button2;
	public Button button3;
	public Button button4;

	DOTweenList dotweenList;

	void Awake()
	{
		DOTween.Init();
		DOTween.defaultAutoPlay = AutoPlay.None;
		DOTween.defaultAutoKill = false;
	}

	void Start()
	{
		dotweenList = new DOTweenList(
			rectA.DOAnchorPosX(600f, 0.6f),
			rectB.DOAnchorPosX(600f, 0.8f).SetId("idTest"),
			rectC.DOAnchorPosX(600f, 1f)
		);

		button1.onClick.AddListener(async() => await PlayTweensForwardAsync());
		button2.onClick.AddListener(async() => await PlayTweensBackwardsAsync());
		button3.onClick.AddListener(async() => await PlayTweensForwardByIdAsync());
		button4.onClick.AddListener(async() => await PlayTweensBackwardsByIdAsync());
	}

	async UniTask PlayTweensForwardAsync()
	{
		message.text = "Playing Forwards...";

		await dotweenList.PlayForward();

		message.text = "All tweens completed.";
	}

	async UniTask PlayTweensBackwardsAsync()
	{
		message.text = "Playing Backwards...";

		await dotweenList.PlayBackwards();

		message.text = "All tweens rewinded.";
	}

	async UniTask PlayTweensForwardByIdAsync()
	{
		message.text = "Playing Forwards...";

		await dotweenList; //.PlayForwardById("idTest");

		message.text = "Tween B completed.";
	}

	async UniTask PlayTweensBackwardsByIdAsync()
	{
		message.text = "Playing Backwards...";

		await dotweenList.PlayBackwardsById("idTest");

		message.text = "Tween B rewinded.";
	}
}

# DOTweenList

DOTween の複数の Tween をまとめて再生したり、待機したりできるようになります。

## 注意

以下のアセットが別途必要です。

- [DOTween](https://assetstore.unity.com/packages/tools/animation/dotween-hotween-v2-27676)
- [UniTask](https://github.com/Cysharp/UniTask)

## 使い方

### 準備

まず、DOTweenList に処理したい Tween を詰めます。以下の 3 つの方法があります：

(i) コンストラクター

```cs
var dotweenList = new DOTweenList(
	rectA.DOAnchorPosX(600f, 0.6f),
	rectB.DOAnchorPosX(600f, 0.8f),
	rectC.DOAnchorPosX(600f, 1f)
);
```

(ii) `Add()`メソッド

```cs
var dotweenList = new DOTweenList();

dotweenList.Add(rectA.DOAnchorPosX(600f, 0.6f));
dotweenList.Add(rectB.DOAnchorPosX(600f, 0.8f));
dotweenList.Add(rectC.DOAnchorPosX(600f, 1f));
```

(iii) `AddTo()`拡張メソッド

```cs
var dotweenList = new DOTweenList();

rectA.DOAnchorPosX(600f, 0.6f).AddTo(dotweenList);
rectB.DOAnchorPosX(600f, 0.8f).AddTo(dotweenList);
rectC.DOAnchorPosX(600f, 1f).AddTo(dotweenList);
```

### 処理を実行

DOTweenList には、以下の処理を実行できます。

```cs
// 再生
dotweenList.PlayForward();

// 逆再生
dotweenList.PlayBackwards();

// 最初に戻す
dotweenList.Rewind();

// 最後に進める
dotweenList.Complete();

// 強制終了
dotweenList.Kill();
```

メソッド名の末尾に`ById`を加えることで、特定の ID の Tween のみに処理を実行できます。

```cs
dotweenList.PlayForwardById("id");

dotweenList.PlayBackwardsById("id");

dotweenList.RewindById("id");

dotweenList.CompleteById("id");

dotweenList.KillById("id");
```

この他、以下のメソッドが用意されています。

```cs
// Tween がどれか一つでも再生中なら true を返す
dotweenList.IsPlaying();

// リストをクリアする
dotweenList.Clear();

// すべての Tween の再生開始から終了までにかかる時間を取得する
// 注意：Tween が再生中であっても、開始からの時間が返る
dotweenList.GetTotalTime();

// 特定の ID を持つすべての Tween の再生にかかる時間を取得する
// 注意：Tween が再生中であっても、開始からの時間が返る
dotweenList.GetTotalTimeById("id");
```

### 待機

`await`を付与することで、再生終了まで待機することができます。

```cs
// 再生終了まで待機する
await dotweenList.PlayForward();

// すでに再生中の Tween を待つこともできる
await dotweenList;
```

`Skippable()`拡張メソッドを用いれば、指定した条件で Tween をスキップすることができます。

```cs
// 再生終了まで待機する。何かキーを入力するとスキップする
await dotweenList.PlayForward().Skippable(() => Input.anyKeyDown);
```

## DOTween Animation

DOTween Pro の機能である DOTween Animation を DOTweenList に詰める場合は、以下のように書きます。

```cs
var dotweenList = new DOTweenList(GetComponent<DOTweenAnimation>().GetTweens());
```

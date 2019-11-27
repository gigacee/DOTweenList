using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UniRx.Async;
using UnityEngine;

public class DOTweenList : CustomYieldInstruction
{
	List<Tween> list;
	public Func<bool> cond;

	public DOTweenList(params Tween[] tweens)
	{
		list = tweens.ToList();
	}

	public DOTweenList(IEnumerable<Tween> tweens)
	{
		list = tweens.Where(x => x != null).ToList();
	}

	public override bool keepWaiting
	{
		get
		{
			if ((cond != null && cond()))
			{
				foreach (var tween in list)
				{
					if (tween.IsActive() && tween.IsPlaying())
					{
						if (tween.IsBackwards())
						{
							tween.Rewind();
						}
						else
						{
							tween.Complete();
						}
					}
				}

				return false;
			}

			return IsPlaying();
		}
	}

	public DOTweenList PlayForward(bool rewind = false)
	{
		foreach (var tween in list)
		{
			if (rewind)
			{
				tween.Rewind();
			}

			tween.PlayForward();
		}

		return this;
	}

	public DOTweenList PlayForwardById(string id, bool rewind = false)
	{
		foreach (var tween in list)
		{
			if (tween.stringId == id)
			{
				if (rewind)
				{
					tween.Rewind();
				}

				tween.PlayForward();
			}
		}

		return this;
	}

	public DOTweenList PlayBackwards(bool complete = false)
	{
		foreach (var tween in list)
		{
			if (complete)
			{
				tween.Complete();
			}

			tween.PlayBackwards();
		}

		return this;
	}

	public DOTweenList PlayBackwardsById(string id, bool complete = false)
	{
		foreach (var tween in list)
		{
			if (tween.stringId == id)
			{
				if (complete)
				{
					tween.Complete();
				}

				tween.PlayBackwards();
			}
		}

		return this;
	}

	public void Rewind()
	{
		foreach (var tween in list)
		{
			tween.Rewind();
		}
	}

	public void RewindById(string id)
	{
		foreach (var tween in list)
		{
			if (tween.stringId == id)
			{
				tween.Rewind();
			}
		}
	}

	public void Complete()
	{
		foreach (var tween in list)
		{
			tween.Complete();
		}
	}

	public void CompleteById(string id)
	{
		foreach (var tween in list)
		{
			if (tween.stringId == id)
			{
				tween.Complete();
			}
		}
	}

	public void Kill()
	{
		foreach (var tween in list)
		{
			tween.Kill();
		}
	}

	public void KillById(string id)
	{
		foreach (var tween in list)
		{
			if (tween.stringId == id)
			{
				tween.Kill();
			}
		}
	}

	public bool IsPlaying() => list.Any(x => x.IsActive() && x.IsPlaying());

	public void Add(Tween tween) => list.Add(tween);

	public void Clear() => list.Clear();

	public float GetTotalTime() => list.Max(x => x.Duration() + x.Delay());

	public float GetTotalTimeById(string id) =>
		list.Where(x => x.stringId == id).Max(x => x.Duration() + x.Delay());
}

public static class DOTweenListExtensions
{
	public static Tween AddTo(this Tween self, DOTweenList dotweenList)
	{
		if (self == null)
		{
			throw new ArgumentNullException("tween");
		}

		if (dotweenList == null)
		{
			throw new ArgumentNullException("dotweenList");
		}

		dotweenList.Add(self);

		return self;
	}

	public static DOTweenList Skippable(this DOTweenList self, Func<bool> cond)
	{
		self.cond = cond;

		return self;
	}

	public static async void Forget(this DOTweenList self)
	{
		await self;
	}
}

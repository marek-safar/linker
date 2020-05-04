﻿using System;

namespace Mono.Linker.Tests.Cases.Expectations.Assertions
{
	[AttributeUsage (AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
	public class DependencyRecordedAttribute : BaseExpectedLinkedBehaviorAttribute
	{
		public DependencyRecordedAttribute (string source, string target)
		{
			if (string.IsNullOrEmpty (source))
				throw new ArgumentException ("Value cannot be null or empty.", nameof (source));

			if (string.IsNullOrEmpty (target))
				throw new ArgumentException ("Value cannot be null or empty.", nameof (target));
		}
	}
}

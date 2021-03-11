// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Reflection;
using Mono.Linker.Tests.Cases.Expectations.Assertions;
using Mono.Linker.Tests.Cases.Expectations.Metadata;

namespace Mono.Linker.Tests.Cases.LinkAttributes
{
	[SetupLinkAttributesFile ("LinkerAttributeRemovalConditional.xml")]
	[IgnoreLinkAttributes (false)]
	class LinkerAttributeRemovalConditional
	{
		public static void Main ()
		{
			new Kept_1 ();
			new Kept_2 ();
			new Kept_3 ();
			new Removed_1 ();
			new Removed_2 ();
		}

		[Kept]
		[TestConditionalRemove ()]
		class Kept_1
		{
		}

		[Kept]
		[TestConditionalRemove ("my-value", "string value")]
		class Kept_2
		{
		}

		[Kept]
		[TestConditionalRemove (1, true)]
		class Kept_3
		{
		}

		[Kept]
		[TestConditionalRemove ("remove", "string value")]
		class Removed_1
		{
		}

		[Kept]
		[TestConditionalRemove ("remove", 1)]
		class Removed_2
		{
		}
	}

	[Kept]
	[KeptBaseType (typeof (System.Attribute))]
	class TestConditionalRemoveAttribute : Attribute
	{
		[Kept]
		public TestConditionalRemoveAttribute ()
		{
		}

		[Kept]
		public TestConditionalRemoveAttribute (string key, string value)
		{
		}

		[Kept]
		public TestConditionalRemoveAttribute (object key, int value)
		{
		}

		[Kept]
		public TestConditionalRemoveAttribute (int key, object value)
		{
		}
	}
}

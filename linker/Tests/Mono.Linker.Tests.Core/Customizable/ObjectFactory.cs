﻿using Mono.Cecil;

namespace Mono.Linker.Tests.Core.Customizable {
	public class ObjectFactory {
		public virtual TestCaseSandbox CreateSandbox (TestCase testCase)
		{
			return new TestCaseSandbox (testCase);
		}

		public virtual TestCaseCompiler CreateCompiler ()
		{
			return new TestCaseCompiler ();
		}

		public virtual LinkerDriver CreateLinker ()
		{
			return new LinkerDriver ();
		}

		public virtual ResultChecker CreateChecker ()
		{
			return new ResultChecker ();
		}

		public virtual TestCaseMetadaProvider CreateMetadatProvider (TestCase testCase, AssemblyDefinition fullTestCaseAssemblyDefinition)
		{
			return new TestCaseMetadaProvider (testCase, fullTestCaseAssemblyDefinition);
		}

		public virtual LinkerArgumentBuilder CreateLinkerArgumentBuilder ()
		{
			return new LinkerArgumentBuilder ();
		}
	}
}
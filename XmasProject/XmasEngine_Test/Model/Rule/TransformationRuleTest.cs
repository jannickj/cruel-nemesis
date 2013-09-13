using System;
using NUnit.Framework;
using XmasEngineModel.Rule;
using XmasEngineModel.Rule.Exceptions;

namespace XmasEngine_Test.Model.Rule
{
	[TestFixture]
	public class TransformationRuleTest
	{
		[Test]
		public void Conclude_containsNumberIsOneCondition_CorrectConclusionWhenGivenOne()
		{
			TransformationRule<int> rule = new TransformationRule<int>();
			rule.AddPremise(i => i == 1, new Conclusion("correct"));

			string expected = "correct";
			string actual = rule.Conclude(1).ToString();

			Assert.AreEqual(expected, actual);
		}


		[Test]
		public void Conclude_containsNumberIsOneCondition_DontCareConclusionWhenGivenATwo()
		{
			TransformationRule<int> rule = new TransformationRule<int>();
			rule.AddPremise(i => i == 1, new Conclusion("Not used"));

			Assert.IsInstanceOf<DontCareConclusion>(rule.Conclude(2));
		}

		[Test]
		public void Conclude_containsTwoCondradictionaryConclusions_RaiseException()
		{
			TransformationRule<int> rule = new TransformationRule<int>();
			rule.AddPremise(i => i == 1, new Conclusion("Correct"));
			rule.AddPremise(i => i == 1, new Conclusion("Not Correct"));

			bool exceptionthrown = false;
			Exception exception = null;
			try
			{
				rule.Conclude(1);
			}
			catch (MultiConclusionException e)
			{
				exceptionthrown = true;
				exception = e;
			}

			Assert.IsTrue(exceptionthrown);
			Assert.IsInstanceOf<MultiConclusionException>(exception);
		}
	}
}
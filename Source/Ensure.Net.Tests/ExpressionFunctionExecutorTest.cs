using System;
using Ensure.Net.Helpers;
using Ensure.Net.Tests.Helpers;
#if Expressions_Supported
using System.Linq.Expressions;
#endif

namespace Ensure.Net.Tests
{
    [TestFixture]
    public class ExpressionFunctionExecutorTest
    {
#if Expressions_Supported
        [Test]
        public void ExecuteFunctionWithExpressionShouldThrowArgumentExceptionIfExpressionIsNull()
        {
            // Arrange
            IEnsurable<string> TestFuncToBeExecuted(string value, string parameterName) => new Ensurable<string>(parameterName);
            Expression<Func<string>> userInputExpression = null;
            IEnsurable<string> FuncToTest() => ExpressionFunctionExecutor.ExecuteFunctionWithExpression(userInputExpression, TestFuncToBeExecuted);

            // Act
            Exception ex = Assert.Throws<ArgumentException>(FuncToTest);

            // Assert
            Assert.Equal("Expression cannot be null.", ex.Message);
        }

        [Test]
        public void ExecuteFunctionWithExpressionShouldNotThrowArgumentExceptionIfExpressionHasNullPassedToIt()
        {
            // Arrange
            IEnsurable<string> TestFuncToBeExecuted(string value, string parameterName) => new Ensurable<string>(parameterName);
            Expression<Func<string>> userInputExpression = () => null;

            // Act
            IEnsurable<string> executeFunctionWithExpression = ExpressionFunctionExecutor.ExecuteFunctionWithExpression(userInputExpression, TestFuncToBeExecuted);

            // Assert
            Assert.Equal("Variable", executeFunctionWithExpression.Value);
        }

        [Test]
        public void ExecuteFunctionWithExpressionShouldThrowArgumentExceptionIfExpressionIsNotMemberExpression()
        {
            // Arrange
            IEnsurable<string> TestFuncToBeExecuted(string value, string parameterName) => new Ensurable<string>(parameterName);
            Expression<Func<string>> userInputExpression = () => string.Empty + string.Empty;
            IEnsurable<string> FuncToTest() => ExpressionFunctionExecutor.ExecuteFunctionWithExpression(userInputExpression, TestFuncToBeExecuted);

            // Act
            Exception ex = Assert.Throws<ArgumentException>(FuncToTest);

            // Assert
            Assert.Equal("Expression must be of type MemberExpression.", ex.Message);
        }

        [Test]
        public void ExecuteFunctionWithExpressionShouldNotThrowAnyExceptionIfExpressionTypeIsValid()
        {
            // Arrange
            string variableToCheck = "MyVariable";
            IEnsurable<string> TestFuncToBeExecuted(string value, string parameterName) => new Ensurable<string>(parameterName);
            Expression<Func<string>> userInputExpression = () => variableToCheck;

            // Act
            IEnsurable<string> executeFunctionWithExpression = ExpressionFunctionExecutor.ExecuteFunctionWithExpression(userInputExpression, TestFuncToBeExecuted);

            // Assert
            Assert.Equal(nameof(variableToCheck), executeFunctionWithExpression.Value);
        }
#endif
    }
}
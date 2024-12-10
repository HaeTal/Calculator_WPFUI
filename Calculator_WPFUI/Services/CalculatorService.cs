using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator_WPFUI.Services
{
    public class CalculatorService
    {
        public List<string> Ops = new List<string> { "+", "-", "*", "/", "(", ")" };
        public Dictionary<string, int> Precs = new Dictionary<string, int>
        {
            ["("] = 0,
            ["+"] = 1,
            ["-"] = 1,
            ["*"] = 2,
            ["/"] = 2,
        };



        public CalculatorService()
        {

        }


        public List<string> Tokenization(string input)
        {
            List<string> tokens = new List<string>();
            StringBuilder numBuffer = new StringBuilder();

            foreach (var c in input)
            {
                if (char.IsWhiteSpace(c))
                {
                    continue;
                }

                // 음수처리
                if (c == '-' && numBuffer.Length == 0)
                {
                    numBuffer.Append("-");

                    continue;
                }

                if (char.IsDigit(c) || c == '.')
                {
                    numBuffer.Append(c);

                    continue;
                }

                // 현재 아이템이 부호인 경우 numBuffer에 쌓인 숫자를 토큰화
                if (numBuffer.Length > 0)
                {
                    tokens.Add(numBuffer.ToString());
                    numBuffer.Clear();
                }

                tokens.Add(c.ToString());
            }

            if (numBuffer.Length > 0)
            {
                tokens.Add(numBuffer.ToString());
            }

            return tokens;
        }

        public List<string> ConvertToPostfix(List<string> tokens)
        {
            List<string> output = new List<string>();

            Stack<string> opStack = new Stack<string>();


            foreach (var item in tokens)
            {
                if (Ops.Contains(item) == false)
                {
                    // 피연산자인 경우
                    output.Add(item);
                }

                else if (item == "(")
                {
                    opStack.Push(item);
                }

                else if (item == ")")
                {
                    while (opStack.Peek() != "(")
                    {
                        output.Add(opStack.Pop());
                    }

                    // 왼쪽 괄호 버림
                    opStack.Pop();
                }

                else
                {
                    while (opStack.Count != 0)
                    {
                        if (Precs[opStack.Peek()] >= Precs[item])
                        {
                            // 스택에 있는 연산자의 우선순위가 자신보다 높거나 같다면 출력 리스트에 이어 붙여준다
                            output.Add(opStack.Pop());
                        }
                        else
                        {
                            break;
                        }
                    }

                    opStack.Push(item);
                }
            }

            // 더 이상 읽을 토큰이 없다면 스택 전부 pop
            while (opStack.Count != 0)
            {
                output.Add(opStack.Pop());
            }


            return output;
        }

        public string CalcPostfix(List<string> postfix)
        {
            Stack<string> numStack = new Stack<string>();


            foreach (var item in postfix)
            {
                // 숫자인 경우
                if (!Ops.Contains(item))
                {
                    numStack.Push(item);
                }
                else
                {
                    var num2 = double.Parse(numStack.Pop());
                    var num1 = double.Parse(numStack.Pop());

                    switch (item)
                    {
                        case "+":
                            numStack.Push((num1 + num2).ToString());
                            break;

                        case "-":
                            numStack.Push((num1 - num2).ToString());
                            break;


                        case "*":
                            numStack.Push((num1 * num2).ToString());
                            break;


                        case "/":
                            numStack.Push((num1 / num2).ToString());
                            break;

                    }
                }
            }

            return numStack.Pop();
        }

    }
}

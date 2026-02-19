using System;
using System.Collections.Generic;

namespace Balta.Desafio.Command
{
    public class EditorInvoker
    {
        private readonly Stack<ICommand> _undoStack = new Stack<ICommand>();
        private readonly Stack<ICommand> _redoStack = new Stack<ICommand>();

        public void ExecuteCommand(ICommand command)
        {
            try
            {
                command.Execute();
                _undoStack.Push(command);
                _redoStack.Clear(); // Nova ação limpa o histórico de refazer
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Invoker] Erro ao executar comando: {ex.Message}");
            }
        }

        public void Undo()
        {
            if (_undoStack.Count > 0)
            {
                var command = _undoStack.Pop();
                try
                {
                    command.Undo();
                    _redoStack.Push(command);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[Invoker] Erro ao desfazer comando: {ex.Message}");
                    // Em caso de erro, talvez devêssemos reinserir na pilha de undo? 
                    // Para simplificar, assumimos que falhou e saiu da pilha.
                }
            }
            else
            {
                Console.WriteLine("[Invoker] Nada para desfazer.");
            }
        }

        public void Redo()
        {
            if (_redoStack.Count > 0)
            {
                var command = _redoStack.Pop();
                try
                {
                    command.Execute();
                    _undoStack.Push(command);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[Invoker] Erro ao refazer comando: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("[Invoker] Nada para refazer.");
            }
        }
    }
}

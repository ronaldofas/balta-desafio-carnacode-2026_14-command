using DesignPatternChallenge;

namespace Balta.Desafio.Command
{
    public class InsertTextCommand : ICommand
    {
        private readonly TextEditor _editor;
        private readonly string _text;
        private int _position;
        private bool _isFirstExecution = true;

        public InsertTextCommand(TextEditor editor, string text)
        {
            _editor = editor;
            _text = text;
        }

        public void Execute()
        {
            if (_isFirstExecution)
            {
                _position = _editor.GetCursorPosition();
                _isFirstExecution = false;
            }
            
            // Garante que o cursor está na posição correta para Redo
            _editor.SetCursorPosition(_position);
            _editor.InsertText(_text);
        }

        public void Undo()
        {
            // Para desfazer inserção, deletamos o texto inserido
            // DeleteText remove caracteres ANTES do cursor, então posicionamos no final da inserção
            _editor.SetCursorPosition(_position + _text.Length);
            _editor.DeleteText(_text.Length);
        }
    }
}

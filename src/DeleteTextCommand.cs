using DesignPatternChallenge;

namespace Balta.Desafio.Command
{
    public class DeleteTextCommand : ICommand
    {
        private readonly TextEditor _editor;
        private readonly int _length;
        private string _deletedText = string.Empty;
        private int _position;
        private bool _isFirstExecution = true;

        public DeleteTextCommand(TextEditor editor, int length)
        {
            _editor = editor;
            _length = length;
        }

        public void Execute()
        {
            if (_isFirstExecution)
            {
                _position = _editor.GetCursorPosition();
                
                // Captura o texto que será deletado para poder restaurar depois
                if (_position >= _length)
                {
                    string content = _editor.GetContent();
                    _deletedText = content.Substring(_position - _length, _length);
                }
                _isFirstExecution = false;
            }

            // Garante posição correta (necessário para Redo)
            _editor.SetCursorPosition(_position);
            _editor.DeleteText(_length);
        }

        public void Undo()
        {
            // Para desfazer, reinserimos o texto deletado na posição original
            // A posição original de inserção é (Position - Length)
            _editor.SetCursorPosition(_position - _length);
            _editor.InsertText(_deletedText);
        }
    }
}

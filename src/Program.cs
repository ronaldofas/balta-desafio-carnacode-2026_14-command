using System;
using DesignPatternChallenge;

namespace Balta.Desafio.Command
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Balta.io - Desafio CarnaCode 2026 - Command Pattern ===\n");
            
            // --- Execução do Código Legado ---
            Console.WriteLine("--- Cenário 1: Código Legado (Sem Undo) ---");
            var legacyApp = new EditorApplication();
            legacyApp.TypeText("Teste Legado");
            legacyApp.ShowContent();
            Console.WriteLine("Tentando desfazer... (não funciona)");
            legacyApp.Undo();
            Console.WriteLine("-------------------------------------------\n");

            // --- Execução com Command Pattern ---
            Console.WriteLine("--- Cenário 2: Command Pattern (Com Undo/Redo) ---");
            
            // 1. Setup
            var editor = new TextEditor();
            var invoker = new EditorInvoker();

            Console.WriteLine("1. Digitando 'Hello World'");
            invoker.ExecuteCommand(new InsertTextCommand(editor, "Hello World"));
            Console.WriteLine($"Conteúdo: '{editor.GetContent()}'");

            Console.WriteLine("\n2. Deletando ' World' (6 chars)");
            // O cursor está no final (11). Deletar 6 remove " World".
            invoker.ExecuteCommand(new DeleteTextCommand(editor, 6)); 
            Console.WriteLine($"Conteúdo: '{editor.GetContent()}'");

            Console.WriteLine("\n3. Aplicando Negrito em 'Hello'");
            invoker.ExecuteCommand(new FormatTextCommand(editor, 0, 5));
            
            Console.WriteLine("\n4. Desfazendo Negrito (Undo)");
            invoker.Undo();

            Console.WriteLine("\n5. Desfazendo Deleção (Undo) -> Deve trazer ' World' de volta");
            invoker.Undo();
            Console.WriteLine($"Conteúdo: '{editor.GetContent()}'");

            Console.WriteLine("\n6. Desfazendo Inserção (Undo) -> Deve limpar tudo");
            invoker.Undo();
            Console.WriteLine($"Conteúdo: '{editor.GetContent()}'");

            Console.WriteLine("\n7. Refazendo Inserção (Redo) -> 'Hello World'");
            invoker.Redo();
            Console.WriteLine($"Conteúdo: '{editor.GetContent()}'");

            Console.WriteLine("\n8. Refazendo Deleção (Redo) -> 'Hello'");
            invoker.Redo();
            Console.WriteLine($"Conteúdo: '{editor.GetContent()}'");

            Console.WriteLine("\n--- Fim da Execução Command Pattern ---");
        }
    }
}

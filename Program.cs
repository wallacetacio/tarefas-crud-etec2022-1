using Tarefas.db;
bool sair = false;
while (!sair)
{
    string opcao = UI.SelecionaOpcaoEmMenu();

    switch (opcao)
    {
        case "L": ListarTodasAsTarefas(); break;
        case "P": ListarTarefasPendentes(); break;
        case "I": ListarTarefasPorId(); break;
        case "D": ListarTarefasPorDescricao(); break;
        case "N": IncluirNovaTarefa(); break;
        case "A": AlterarDescricaoDaTarefa(); break;
        case "C": ConcluirTarefa(); break;
        case "E": ExcluirTarefa(); break;

        case "S":
            sair = true;
            break;

        default:
            UI.ExibeErro("\nOpção não reconhecida.");
            break;
    }

    Console.Write("\nPressione uma tecla para continuar...");
    Console.ReadKey();
}

void ListarTodasAsTarefas()
{
    UI.ExibeDestaque("\n-- Listar todas as Tarefas ---");
    // Continue daqui
    using (var _db = new tarefasContext()){

        var todasTarefas = _db.Tarefa.ToList<Tarefa>();

        int quantidadeRegistros  = todasTarefas.Count();
        Console.WriteLine($"Encontrada(s) {quantidadeRegistros} tarefas");

        foreach (var tarefa in todasTarefas)
        {

            string concluida = tarefa.Concluida ? ":-)" : ":-(";
            Console.WriteLine($"[{concluida}] número = {tarefa.Id} => {tarefa.Descricao}");
        }
    }
}

void ListarTarefasPendentes()
{
    UI.ExibeDestaque("\n-- Listar Tarefas Pendentes ---");
    // Continue daqui

    using (var _db = new tarefasContext()){
    var todasTarefas = _db.Tarefa
        .Where(t => !t.Concluida)
        .OrderByDescending(t => t.Id)
        .ToList<Tarefa>();

        int quantidadeRegistros  = todasTarefas.Count();
        Console.WriteLine($"Encontrada(s) {quantidadeRegistros} tarefas");

        foreach (var tarefa in todasTarefas)
        {

            string concluida = tarefa.Concluida ? ":-)" : ":-(";
            Console.WriteLine($"[{concluida}] número = {tarefa.Id} => {tarefa.Descricao}");
        }
}
}

void ListarTarefasPorDescricao()
{
    UI.ExibeDestaque("\n-- Listar Tarefas por Descrição ---");
    string descricao = UI.SelecionaDescricao();
    // Continue daqui
    using (var _db = new tarefasContext()){
    var todasTarefas = _db.Tarefa
        .Where(t => t.Descricao.Contains(descricao))
        .OrderBy(t => t.Descricao)
        .ToList<Tarefa>();

        int quantidadeRegistros  = todasTarefas.Count();
        Console.WriteLine($"Encontrada(s) {quantidadeRegistros} tarefas");

        foreach (var tarefa in todasTarefas)
        {

            string concluida = tarefa.Concluida ? ":-)" : ":-(";
            Console.WriteLine($"[{concluida}] número = {tarefa.Id} => {tarefa.Descricao}");
        }
}

    Console.WriteLine(descricao);
}

void ListarTarefasPorId()
{
    UI.ExibeDestaque("\n-- Listar Tarefas por Id ---");
    int id = UI.SelecionaId();
    // Continue daqui

    using (var _db = new tarefasContext()){

        var tarefa = _db.Tarefa.Find(id);
        
        if (tarefa == null){
            Console.WriteLine("Tarefa não encontrada!");
            return;
        }

        string concluida = tarefa.Concluida ? ":-)": ":-(";
        Console.WriteLine($"[{concluida}] número = {tarefa.Id} => {tarefa.Descricao}");

    /*var todasTarefas = _db.Tarefa
        .Where(t => t.Id.Equals(id))
        .OrderByDescending(t => t.Id)
        .ToList<Tarefa>();

        int quantidadeRegistros  = todasTarefas.Count();
        Console.WriteLine($"Encontrada(s) {quantidadeRegistros} tarefas");

        foreach (var tarefa in todasTarefas)
        {

            string concluida = tarefa.Concluida ? ":-)" : ":-(";
            Console.WriteLine($"[{concluida}] número = {tarefa.Id} => {tarefa.Descricao}");
        }*/
}

    Console.WriteLine(id);
}

void IncluirNovaTarefa()
{
    UI.ExibeDestaque("\n-- Incluir Nova Tarefa ---");
    string descricao = UI.SelecionaDescricao();
    // Continue daqui

    if (string.IsNullOrEmpty(descricao)){
        Console.WriteLine("Não é possível cadastrar tarefa sem descrição!");
        return;
    }

    using (var _db = new tarefasContext()){
        var tarefa = new Tarefa{
            Descricao = descricao
        };
        
        _db.Tarefa.Add(tarefa);
        _db.SaveChanges();

        string concluida = tarefa.Concluida ? ":-)" : ":-(";
        Console.WriteLine($"[{concluida}] número = {tarefa.Id} => {tarefa.Descricao}");

    }

    
    Console.WriteLine(descricao);
}

void AlterarDescricaoDaTarefa()
{
    UI.ExibeDestaque("\n-- Alterar Descrição da Tarefa ---");
    int id = UI.SelecionaId();
    string descricao = UI.SelecionaDescricao();
    // Continue daqui

    using (var _db = new tarefasContext()){

        if (string.IsNullOrEmpty(descricao)){
        Console.WriteLine("Não é possível cadastrar tarefa sem descrição!");
        return;
    }

         var tarefa = _db.Tarefa.Find(id);
         
         if (tarefa == null){
            Console.WriteLine("Tarefa não encontrada!");
            return;
        }

        tarefa.Descricao = descricao;
        _db.SaveChanges();

        string concluida = tarefa.Concluida ? ":-)": ":-(";
        Console.WriteLine($"[{concluida}] número = {tarefa.Id} => {tarefa.Descricao}");
    }
    

    
    Console.WriteLine(id);
    Console.WriteLine(descricao);
}

void ConcluirTarefa()
{
    UI.ExibeDestaque("\n-- Concluir Tarefa ---");
    int id = UI.SelecionaId();
    // Continue daqui

     using (var _db = new tarefasContext()){

        
            var tarefa = _db.Tarefa.Find(id);
         
         if (tarefa == null){
            Console.WriteLine("Tarefa não encontrada!");
            return;
        }

        if (tarefa.Concluida){
            Console.WriteLine("Tarefa já concluida!");
            return;
        }

        tarefa.Concluida = true;
        _db.SaveChanges();

        string concluida = tarefa.Concluida ? ":-)": ":-(";
        Console.WriteLine($"[{concluida}] número = {tarefa.Id} => {tarefa.Descricao}");
    }
    Console.WriteLine(id);
}

void ExcluirTarefa()
{
    UI.ExibeDestaque("\n-- Excluir Tarefa ---");
    int id = UI.SelecionaId();
    // Continue daqui

    using (var _db = new tarefasContext()){

        var tarefa = _db.Tarefa.Find(id);
        
        if (tarefa == null){
            Console.WriteLine("Tarefa não encontrada!");
            return;
        }

        _db.Tarefa.Remove(tarefa);
        _db.SaveChanges();

    Console.WriteLine("Tarefa excluida!");
}
}

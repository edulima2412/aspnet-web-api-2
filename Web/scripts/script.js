var tbody = document.querySelector('table tbody');
var aluno = {};

function Cadastrar() {
    aluno.nome = document.querySelector('#nome').value;
    aluno.sobrenome = document.querySelector('#sobrenome').value;
    aluno.telefone = document.querySelector('#telefone').value;
    aluno.registro = document.querySelector('#registro').value;

    if (aluno.id === undefined || aluno.id === 0) {
        SalvarAluno('POST', 0, aluno);
    } else {
        SalvarAluno('PUT', aluno.id, aluno);
    }
    CarregarAlunos();

    $('#myModal').modal('hide')
}

function NovoAluno() {
    var btnSalvar = document.querySelector('#btnSalvar');
    var tituloModal = document.querySelector('#tituloModal');

    document.querySelector('#nome').value = '';
    document.querySelector('#sobrenome').value = '';
    document.querySelector('#telefone').value = '';
    document.querySelector('#registro').value = '';

    aluno = {};

    btnSalvar.textContent = 'Cadastrar';
    tituloModal.textContent = 'Cadastrar Aluno';
    
    $('#myModal').modal('show')
}

function CarregarAlunos() {
    tbody.innerHTML = '';
    var xhr = new XMLHttpRequest();
    xhr.open('GET', `https://localhost:44333/api/Aluno/Recuperar`, true);
    xhr.setRequestHeader(
        "Authorization",
        sessionStorage.getItem('token')
      );

    xhr.onerror = function() {
        console.log('Erro', xhr.readyState);
    }

    xhr.onreadystatechange = function () {
        if(this.readyState == 4) {
            if (this.status == 200){
                var alunos = JSON.parse(this.responseText);
                for (var indice in alunos) {
                    AdicionarLinha(alunos[indice]);
                }
            } else if(this.status == 500 ||this.status == 400) {
                var erro = JSON.parse(this.responseText);
                console.log(erro);
            }
        }
    }
    xhr.send();
}

function SalvarAluno(metodo, id, corpo) {
    var xhr = new XMLHttpRequest();

    if(metodo == 'PUT'){
        xhr.open(metodo, `https://localhost:44333/api/Aluno/${id}`, false);
    } else {
        xhr.open(metodo, `https://localhost:44333/api/Aluno`, false);
    }
    
    xhr.setRequestHeader('content-type', 'application/json');
    xhr.send(JSON.stringify(corpo));
}

CarregarAlunos();

function AdicionarLinha(aluno) {
    var trow = `<tr>
                    <td>${aluno.nome}</td> 
                    <td>${aluno.sobrenome}</td> 
                    <td>${aluno.telefone}</td> 
                    <td>${aluno.registro}</td>
                    <td>
                        <button class="btn btn-info" data-toggle="modal" data-target="#myModal" onclick='EditarAluno(${JSON.stringify(aluno)})'>Editar</button>
                        <button class="btn btn-danger" onclick='Excluir(${JSON.stringify(aluno)})'>Excluir</button>
                    </td>
                </tr>`;
    tbody.innerHTML += trow;
}

function EditarAluno(estudante) {
    var btnSalvar = document.querySelector('#btnSalvar');
    var tituloModal = document.querySelector('#tituloModal');

    document.querySelector('#nome').value = estudante.nome;
    document.querySelector('#sobrenome').value = estudante.sobrenome;
    document.querySelector('#telefone').value = estudante.telefone;
    document.querySelector('#registro').value = estudante.registro;

    aluno = estudante;

    btnSalvar.textContent = 'Salvar';

    tituloModal.textContent = `Editar Aluno ${estudante.nome}`;
}

function DeletarAluno(id) {
    var xhr = new XMLHttpRequest();

    xhr.open('DELETE', `https://localhost:44333/api/Aluno/${id}`, false);
    xhr.send();
}

function Excluir(aluno) {
    bootbox.confirm({
        message: `Tem certeza que deseja excluir ${aluno.nome} ?`,
        closeButton: false,
        buttons: {
            confirm: {
                label: 'Sim',
                className: 'btn-success'
            },
            cancel: {
                label: 'Não',
                className: 'btn-danger'
            }
        },
        callback: function (result) {
            if(result){
                DeletarAluno(aluno.id);
                CarregarAlunos();
            }
        }
    });
    
}

function Cancelar() {
    var btnSalvar = document.querySelector('#btnSalvar');
    var tituloModal = document.querySelector('#tituloModal');

    document.querySelector('#nome').value = '';
    document.querySelector('#sobrenome').value = '';
    document.querySelector('#telefone').value = '';
    document.querySelector('#registro').value = '';

    aluno = {};

    btnSalvar.textContent = 'Cadastrar';
    tituloModal.textContent = 'Cadastrar Aluno';
    
    $('#myModal').modal('hide')
}
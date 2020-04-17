# Wolf and Sheep

### Grupo 08: 
|Nome|Número|GitHub|
|:-:|:-:|:-:|
|Luiz Santos|a21901441|JundMaster|
|Pedro Marques|a21900253|pmarques93|
|Gonçalo Verde|a21901395|MrVerdinsky|

---
### Tarefas realizadas no exercício
A maior parte das tarefas foram realizadas por mais de um elemento.

Tarefas realizadas por ordem cronológica
|Gonçalo Verde|Luiz Santos|Pedro Marques|
|:-:|:-:|:-:|
|Input inicial do Utilizador|Classe Square|Classe Square|
|Criação das ovelhas|_Gameloop_ geral|Criação do Lobo/Método _Intro_ /Método _Victory_|
|_Gameloop_ das ovelhas|Movimentação com input|_Gameloop_ do lobo|
|Limitação do movimento das ovelhas|_Try-Catch_ do lobo|Limitação do movimento do lobo|
|Método _WolfGameOver_|_Try-Catch_ das ovelhas|Método _PrintBoard_|
|Método _SheepGameOver_ / Método SheepChosen|Método _CheckConvert_|Método _LegalMove_|
|Documentação XML|Fluxograma|Relatório|

---
### Repositório git
https://github.com/JundMaster/Projeto-LP1.git

---
### Descrição da solução
Para a resolução deste exercício começámos por criar uma classe _Square_ para cada quadrado do tabuleiro, de modo a termos acesso a determinadas características dos mesmos, como a _row_, _column_ e _isPlayable_ para saber se está ocupado. A lógica foi  que o _Square_ - ou não pode ser jogado, ou pode ser jogado, ou está ocupado por um animal -. Após a criação da classe, construímos uma função com um array bidemensional  para criar um tabuleiro 8x8, onde todo o jogo vai decorrer. Este tabuleiro vai ser desenhado com um _for loop_, numa função à parte, onde vão ser apresentados todos os quadrados do tabuleiro juntamente com os animais.

Após isto optámos por criar os _loops_ de _gameplay_ com _do whiles_, nos quais começámos por criar o _input_ do utilizador para a movimentação tanto do lobo como das ovelhas, em que utilizámos os números da _column_ e da _row_ (da classe _Square_) para mudar a posição dos mesmos. De modo a controlar os movimentos dos animais, utilizamos condições que são aceites apenas quando a jogada é possível (_isPlayable==true_), tendo sido esta condição na qual nos baseámos para a maior parte do código. As jogadas são controladas através de números par/ímpar, que vão definir se é o turno do lobo ou das ovelhas. O código para o lobo e para as ovelhas é semelhante em todos os aspetos. 

Entretanto, de modo a limitar _inputs_ não desejados do utilizador, criámos um _try-catch_ que imprime mensagens de erro caso a opção introduzida não seja uma possibilidade. Posteriormente foi criado o método _CheckConvert_ para este efeito.

Mais tarde, para uma melhor organização do código, dividimos as "responsabilidades" em métodos que executam a movimentação dos animais e condições que terminam o jogo.

---
### Arquitetura do código
#### Métodos:
- #### _Main()_;
  - O método corre a _intro()_ e a _game()_;

- #### _Game()_;
  - Responsável pela _loop_ inteiro do jogo. 

- #### _Intro()_;
  - Imprime regras do jogo no ínicio;

- #### _Victory()_;
  - Imprime mensagem de vitória no final do jogo;

- #### _LegalMove()_;
  - Só aceita números com +1/-1 que a casa atual;

- #### _WolfGameOver()_;
  - Verifica se o lobo ainda tem jogadas possíveis, caso não tenha o jogo é terminado;

- #### _SheepGameOver()_;
  - Verifica se a ovelha ainda tem jogadas possíveis, caso não tenha o jogo é terminado;

- #### _SheepChosen()_;
  - Verifica qual a ovelha que o jogador escolheu;

- #### _PrintBoard()_;
  - Imprime o tabuleiro do jogo;

- #### _CheckConvert()_;
  - Imprime mensagens de erros se o _input_ do utilizar não estiver correto;

- #### _FirstTurn()_;
  - Primeiro turno do jogo;

- #### _WolfTurn()_;
  - Turnos do lobo;

- #### _SheepTurn()_;
  - Turnos da ovelha;

#### Classes:
- #### _Square_;
  - Contém a _row_, _column_ e _isPlayable_;
  
---
### Fluxograma

---
### Trocas de ideias/referências

#### Criação da classe _Square_
- Sluiter, Shad."C# Chess Board 02 board cell classes" _Youtube_, uploaded by shad sluiter, Jun 13, 2019, https://www.youtube.com/watch?v=SFMVyiJ2S6g&feature=youtu.be
#### Try-Catch
- "C# documentation", _Microsoft_, Microsoft 2020,
https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/try-catch

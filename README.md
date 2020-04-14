# Wolf and Sheep

### Grupo 08: 

Gonçalo Verde (a21901395)\
Pedro Marques (a21900253)\
Luiz Santos (a21901441)

---
### Tarefas realizadas no exercício
|Gonçalo Verde|Luiz Santos|Pedro Marques|
|---	|---	|---	|
|Input inicial do Utilizador|Classe Square|Classe Square|
|Criação das ovelhas|_Gameloop_ geral|Criação do Lobo/Método _intro_ /Método _victory_|
|_Gameloop_ das ovelhas|Movimentação com input|_Gameloop_ do wolf|
|Limitação do movimento das ovelhas|Try-Catch|Limitação do movimento do lobo|
|||Método _printBoard_|

---
### Repositório git
https://github.com/JundMaster/Projeto-LP1.git

---
### Descrição da solução
Para a resolução deste exercício começámos por criar uma classe _Square_ para cada "quadrado" do tabuleiro, de modo a termos acesso a determinadas características dos mesmos, como a _row_, _column_ e _isPlayable_ para saber se está ocupado. A nossa lógica foi de que o _Square_ - ou não pode ser jogado, ou pode ser jogado, ou está ocupado por um animal -. Após a criação da classe, construímos uma função com um array bidemensional  para criar um tabuleiro 8x8, onde todo o jogo vai decorrer. Este tabuleiro vai ser desenhado com um _for loop_, numa função à parte, onde vão ser apresentados todos os quadrados do tabuleiro juntamente com os animais.

Após isto optámos por criar os _loops_ de _gameplay_ com _do whiles_, nos quais começámos por criar o _input_ do utilizador para a movimentação tanto do lobo como das ovelhas, em que utilizámos os números da _column_ e da _row_ (da classe _Square_) para mudar a posição dos mesmos. De modo a limitar os movimentos, utilizamos condições que são aceites apenas quando a jogada é possível (_isPlayable==true_). As jogadas são controladas através de números par/ímpar, que vão definir se é o turno do lobo ou das ovelhas. O código para o lobo e para as ovelhas é semelhante em todos os aspetos. 

Para uma melhor organização do código criámos também alguns métodos simples, como o _intro()_, o _victory()_ e _printBoard()_ que servem apenas para imprimir algum texto em determinadas condições.

---
### Arquitetura do código
#### Métodos:
- #### _main()_;
  - A função apenas corre a _intro()_ e a _game()_;

- #### _game()_;
  - Responsável pela _loop_ inteiro do jogo. 
  - Contém todas as variáveis necessárias para o programa. 
  - Recebe inputs e imprime o tabuleiro de jogo;

- #### _intro()_;
  - Imprime regras do jogo no ínicio;

- #### _victory()_;
  - Imprime mensagem de vitória no final do jogo;
  
- #### _printBoard()_;
  - Imprime o tabuleiro do jogo;

#### Classes:
- #### _Square_;
  - Contém a _row_, _column_ e _isPlayable;

---
### Fluxograma

---
### Trocas de ideias/referências

#### Criação da classe _Square_
- Sluiter, Shad."C# Chess Board 02 board cell classes" _Youtube_, uploaded by shad sluiter, Jun 13, 2019, https://www.youtube.com/watch?v=SFMVyiJ2S6g&feature=youtu.be
#### Try-Catch
- "C# documentation", _Microsoft_, Microsoft 2020,
https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/try-catch

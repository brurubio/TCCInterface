<img src="WindowsFormsApplication1/Images/Bug-LineMaxSize.png" align="center" alt="Footer"/>

#TCCInterface

Este projeto compõe um Trabalho de Conclusão de Curso do curso de Ciência da Computação da Universidade Estadual Paulista (UNESP) 
Campus Bauru. O trabalho em questão é da área de segurança de redes de computadores, com foco no processo de detecção de anomalias.

O principal objetivo deste trabalho foi propor uma abordagem baseada na classificação e otimização das características dos pacotes 
capturados na rede. A etapa de classificação é responsável por aprender o que é anomalia, e assim, ter a capacidade de classificar 
novas amostras em duas possíveis classes: anomalia ou não anomalia, e a outra etapa é responsável pela otimização da função que 
representa tais características, com o intuito de encontrar o melhor valor para aumentar a acurácia na detecção.

## Classificação
O classificador utilizado foi baseado em Florestas de Caminhos Ótimos (*Optimum-Path Forest* - OPF), o qual utiliza uma metodologia 
baseada em grafos e seu funcionamento parte do princípio da seleção de protótipos (amostras mais representativas das classes) os quais 
devem competir entre si para conquistar os nós adjacentes, definindo, então, suas classes. <cite>[1]</cite>

## Otimização

A otimização utilizada foi a Otimização por Enxame de Partículas (*Particle Swarm Optimization* - PSO), a qual é baseada no 
comportamento social biológico, especificamente no comportamento social de bandos de pássaros e cardumes de peixes, os quais têm 
capacidade de aprender e transmitir conhecimento para outros indivíduos da população. Este compartilhamento de conhecimento influencia 
as decisões dos indivíduos, que buscam seguir a melhor opção. <cite>[2]</cite>

## Interface Gráfica

Com o intuito de exemplificar, de forma mais visual, as técnicas aplicadas no trabalho, foi criado um software que auxilia o usuário
a executar o procedimento. O software foi desenvolvido em linguagem C# para ambiente linux. Seu funcionamento é simples, criando apenas um *front-end* para o processamento realizado em *back-end* através do terminal de comando.

### Ferramentas Adicionais

Entretanto, para o correto funcionamento da interface há alguns pré-requistos, tais como, o compilador de C# integrado com .NET Framework <cite>[3]</cite>:
```
# sudo apt-get install mono-complete
```

E além do compilador, as bibliotecas que contém o OPF e o PSO são necessárias:
* LibOPF: https://github.com/jppbsi/LibOPF
* LibOPT-Plus: https://github.com/jppbsi/LibOPT-plus
* LibDEEP: https://github.com/jppbsi/LibDEEP
* LibDEV: https://github.com/jppbsi/LibDEV

devem ser baixadas e instaladas de acordo com as instruções presentes em suas respectivas wikis.

### Formato dos Dados

Para utilizar o OPF, é necessário utilizar um formato específico na base de dados, já que o classificador compreende características numéricas. O formato aceito é o binário, ou um arquivo texto que será convertido internamente. Um exemplo pode ser visto a seguir:

|<# número de amostras> <# número de classes> <# número de características>|
----------------------------------------------------------------------------
|\<0> \<rótulo> \<característica 1 para a amostra 0> \<característica 2 para a amostra 0> ...|
|...|
|\<n-1> \<rótulo> \<característica 1 para a amostra n-1> \<característica 2 para a amostra n-1> ...|

## Referências Bibliográficas

[1] PAPA, J. P.; FALCãO, A. X.; SUZUKI, C. T. N. Supervised pattern classification based on optimum-path forest. International Journal 
of Imaging Systems and Technology, Wiley Subscription Services, Inc., A Wiley Company, v. 19, n. 2, p. 120–131, 2009. ISSN 1098-1098.

[2] KENNEDY, J.; EBERHART, R. Particle swarm optimization. In: Neural Networks, 1995. Proceedings., IEEE International Conference on. 
[S.l.: s.n.], 1995. v. 4, p. 1942–1948 vol.4.

[3] Mono. Disponível em: http://www.mono-project.com/

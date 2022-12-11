# Japim: Construtor de Projetos

## Para que serve?

Japim é uma ferramenta para criação de esqueleto de  aplicações de software, através de uma linguagem declarativa simples. Permite uma melhor visão e elaboração da arquitetura do software durante seu design.

Frameworks como Angular, Unity3D e Spring constroem automaticamente um aplicativo base para os desenvolvedores trabalharem sobre, o que corta o trabalho de criar diversos diretórios e arquivos a mão logo de inicio. Isto é uma grande vantagem para aplicações com potencial de complexidade alto.
Japim oferece uma alternativa semelhante para aplicações menos robustas, que não usam de padrões já definidos pelas ferramentas de programação.

## Linguagem

Japim implementa uma linguagem declarativa que é interpretada para a geração da estrutura do projeto.

Exemplo:
```
* Comentários *

[root]			* Tudo abixo de [root] refere-se a raiz da aplicação *

> MyApp [		* Aqui abre-se um diretório *
+ style.css		* Aqui declara-se um arquivo dentro de MyApp *
+ index.html	* Mesma operação anterior *
> scripts [		* Aqui abre-se um subdiretório dentro de MyApp *
+ script.js		* Aqui declara-se um novo arquivo dentro de MyApp/scripts *
]				* Fecha-se o subdiretório *
]				* Fecha-se MyApp *
```

## Como usar?

No terminal faça:
```

$japim file
```

Então será gerada a estrutura declarada em `file`, caso nehum erro seja encontrado.

Orion Query Language
----

## Overview
Orion Query Language は、 Orion ステータスをフィルタリングするために使用されるクエリ言語です。  
タイムラインの構成、ミュート、検索などに使われています。

以下のように、 SQL における SELECT ステートメントのような文法で記述することが出来ます。  
なお、 `FROM` や `WHERE` といったキーワードや演算子は、大文字小文字を区別しません。

```sql
/* 本文に `Mastodon` が含まれるもののみを抽出 */
FROM federated WHERE body contains "Mastodon"
```

`FROM` 以下をソースクエリ、 `WHERE` 以下をフィルタクエリといいます。
ソースクエリおよびフィルタクエリはどちらかを省略して使用することが出来ます。

```sql
/* GNU social, Mastodon の Federated タイムライン構成クエリ */
FROM federated
```


## Source
ソースクエリは、ステータスの取得元を指定します。  
ソースクエリは、クエリ部分だけでなく、タイムラインに関連付けられているプロバイダ情報とともに構成されます。  
省略した場合、 `*` が使用されます。

例えば、ソースクエリが以下のような場合:

```sql
FROM fedreated
```

タイムラインのプロバイダが Mastodon ならば、 Mastodon の連合タイムライン、GNU social ならば、 GNU social の連合タイムラインとなります。


### ソース一覧
利用可能なソース一覧は以下の通りです。  
なお、一部ソースには、それぞれサービスプロバイダの API レートリミットが適用されます。


| Source      | `home` | `public` | `federated` | `*` | `mentions` | `messages` | `notifications` |
| :---------- | :----: | :------: | :---------: | :-: | :--------: | :--------: | :-------------: |
| Twitter     | x      | <-       | <-          | <-  | x          | x          | x               |
| Croudia     | x      | x        | <-          | <-  | x          | x          |                 |
| GNU social  | x      | x        | x           | <-  | x          |            |                 |
| Mastodon    | x      | x        | x           | <-  | x          |            | x               |

プロバイダがサポートしていないソースを指定した場合、ステータスは取得されません。  
また、一部ソースは、 Orion を起動する以前のステータスは取得されないものがあります。


### `home`
ホームタイムラインを取得します。  
一般的には、フォローしているユーザーのステータスおよび、リブログされたステータスが対象となります。


### `public`
パブリックタイムラインを取得します。  
OStatus に対応しているサービスの場合は、該当サービス全ユーザーのステータスとリブログが含まれます。


### `federated`
連合タイムラインを取得します。  
該当サービス全ユーザーのステータス、ユーザーがフォローしている他サービスのステータスおよびそのリブログが含まれます。


### `*`
プロバイダがサポートしているソースのうち、最も多くステータスが取得できるソースを指します。  
例えば、 Twitter の場合は `home` となります。


### `mentions`, `mention`
認証ユーザーへの返信が含まれるタイムラインを取得します。


### `messages`, `message`
ダイレクトメッセージを取得します。


### `notifications`, `notification`
通知を取得します。


## Filter
ステータスをフィルタリングするクエリです。  
省略した場合は `()` となります。  

フィルタクエリは、結果が `boolean` (論理型) になっている必要があります。  
例えば、 `1 + 1` の結果は `numeric` (数値型) であり、 `boolean` ではありません。


### 型
Orion Query Language には、基本として以下の型が存在します。

* `boolean` - `True` もしくは `False` の値を持つ型です。
* `numeric` - `10` や `6` といった、数値です。
* `string` - `"hoge"` などの文字や文字列です。

また、追加で以下の型が存在します。

* `Status`
* `User`


#### `Status` 型
ステータスそのものを表します。  
`WHERE ReblogsCount > 10` のように使用できます。

以下のプロパティを持っています。

| Prop             | Type      |
| :--------------- | :-------: |
| `Id`             | `numeric` |
| `CreatedAt`      | `numeric` |
| `User`           | `User`    |
| `Body`           | `string`  |
| `ReblogsCount`   | `numeric` |
| `FavoritesCount` | `numeric` |
| `IsReblogged`    | `boolean` |
| `IsFavorited`    | `boolean` |
| `Source`         | `string`  |


#### `User` 型
ユーザーを表します。  
`User` を介することで使用できます。  
例:

```
WHERE User.StatusesCount > 10`
```


以下のプロパティを持っています。

| Prop             | Type      |
| :--------------- | :-------: |
| `Id`             | `numeric` |
| `CreatedAt`      | `numeric` |
| `ScreenName`     | `string`  |
| `Username`       | `string`  |
| `Location`       | `string`  |
| `Description`    | `string`  |
| `Url`            | `string`  |
| `FollowersCount` | `numeric` |
| `FriendsCount`   | `numeric` |
| `StatusesCount`  | `numeric` |
| `FavoritesCount` | `numeric` |


### 演算子
フィルタクエリは、演算子を用いて、最終結果が `boolean` である必要があります。  
なお、フィルタクエリは `()` で囲むことができ、囲まれた部分は先に計算されます。


#### `!`
`boolean` の値を否定します。

| 項        | 結果      |
| :-------: | :-------: |
| `boolean` | `boolean` |

例えば、 `!IsReblogged` とすると、リブログしたもの**以外**が取得されます。


#### `+`
両項の値を加算します。

| 左項      | 右項      | 結果      |
| :-------: | :-------: | :-------: |
| `numeric` | `numeric` | `numeric` |
| `string`  | `string`  | `string`  |


#### `-`
両項の値を減算します。

| 左項      | 右項      | 結果      |
| :-------: | :-------: | :-------: |
| `numeric` | `numeric` | `numeric` |


#### `*`
両項の値を乗算します。

| 左項      | 右項      | 結果      |
| :-------: | :-------: | :-------: |
| `numeric` | `numeric` | `numeric` |


#### `/`
両項の値を除算します。

| 左項      | 右項      | 結果      |
| :-------: | :-------: | :-------: |
| `numeric` | `numeric` | `numeric` |


#### `contains`
左項の内容に右項が含まれるかどうかを計算します。

| 左項      | 右項      | 結果      |
| :-------: | :-------: | :-------: |
| `string`  | `string`  | `boolean` |


#### `containsIgnoreCase`
左項の内容に右項が含まれるかどうかを計算します。  
このとき、文字列の大文字小文字は**区別しません**。

| 左項      | 右項      | 結果      |
| :-------: | :-------: | :-------: |
| `string`  | `string`  | `boolean` |


#### `startsWith`
左項の内容が、右項の内容から始まるかどうかを計算します。

| 左項      | 右項      | 結果      |
| :-------: | :-------: | :-------: |
| `string`  | `string`  | `boolean` |


#### `startsWithIgnoreCase`
左項の内容が、右項の内容から始まるかどうかを計算します。  
このとき、文字列の大文字小文字は**区別しません**。

| 左項      | 右項      | 結果      |
| :-------: | :-------: | :-------: |
| `string`  | `string`  | `boolean` |


#### `endsWith`
左項の内容が、右項の内容で終わるかどうかを計算します。

| 左項      | 右項      | 結果      |
| :-------: | :-------: | :-------: |
| `string`  | `string`  | `boolean` |


#### `endsWithIgnoreCase`
左項の内容が、右項の内容で終わるかどうかを計算します。  
このとき、文字列の大文字小文字は**区別しません**。

| 左項      | 右項      | 結果      |
| :-------: | :-------: | :-------: |
| `string`  | `string`  | `boolean` |



#### `<`
左項の内容と右項の内容を比較し、左項が右項よりも小さいかどうかを計算します。

| 左項      | 右項      | 結果      |
| :-------: | :-------: | :-------: |
| `numeric` | `numeric` | `boolean` |


#### `<=`
左項の内容と右項の内容を比較し、左項が右項よりも小さいか、もしくは等しいかどうかを計算します。

| 左項      | 右項      | 結果      |
| :-------: | :-------: | :-------: |
| `numeric` | `numeric` | `boolean` |


#### `>`
左項の内容と右項の内容を比較し、右項が左項よりも小さいかどうかを計算します。

| 左項      | 右項      | 結果      |
| :-------: | :-------: | :-------: |
| `numeric` | `numeric` | `boolean` |


#### `>=`
左項の内容と右項の内容を比較し、右項が左項よりも小さいか、もしくは等しいかどうかを計算します。

| 左項      | 右項      | 結果      |
| :-------: | :-------: | :-------: |
| `numeric` | `numeric` | `boolean` |


#### `=`, `==`
左項の内容と右項の内容が等しいかを計算します。

| 左項      | 右項      | 結果      |
| :-------: | :-------: | :-------: |
| `boolean` | `boolean` | `boolean` |
| `numeric` | `numeric` | `boolean` |
| `string`  | `string`  | `boolean` |


#### `!=`
左項の内容と右項の内容が等しくないかを計算します。

| 左項      | 右項      | 結果      |
| :-------: | :-------: | :-------: |
| `boolean` | `boolean` | `boolean` |
| `numeric` | `numeric` | `boolean` |
| `string`  | `string`  | `boolean` |


#### `&`, `&&`
左項の内容と右項の内容の論理積 (AND) を取得します。  
`True && True` 以外は全て `False` となります。

| 左項      | 右項      | 結果      |
| :-------: | :-------: | :-------: |
| `boolean` | `boolean` | `boolean` |



#### `|`, `||`
左項の内容と右項の内容の論理和 (OR) を取得します。  
`False || False` 以外は全て `True` となります。

| 左項      | 右項      | 結果      |
| :-------: | :-------: | :-------: |
| `boolean` | `boolean` | `boolean` |


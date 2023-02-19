# 開発者向け情報

## 開発環境
無料で入手でき、商用利用の制限がなく、シェアの高いツールを標準開発環境とします。
- OS：Windows 11
- コンパイラ：Net 6 SDK
- エディタ：Visual Studio Code
- デバッガ：C# extension for Visual Studio Code
- 構成管理ツール：Git for Windows, TortoiseGit
- リポジトリ：GitHub
- CI/CD：GitHub Actions

## 品質目標
ソースコードの記法について C# コーディングガイドを尊重します。
Github Actions を使って、Win, Linux, ホスト上でビルドを確認します
テストでは模擬　VXI-11 サーバを使用し、上記フィーチャーの動作を確認します。
テストカバレッジは正常動作パスのカバレッジ 100%、異常動作パスのカバレッジ0%をリリース指標とします。

## 文書目標
説明は簡潔で1ページを2000文字以内にします。

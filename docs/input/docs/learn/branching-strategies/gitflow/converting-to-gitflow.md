---
Order: 60
Title: Converting to GitFlow
RedirectFrom: docs/git-branching-strategies/converting-to-gitflow
---

Converting to GitFlow is simple. Whenever you need to convert, simply do the
following

```shell
git checkout main
git checkout -b develop
git push upstream develop
```

Afterwards you need to set `develop` to be your default branch. And now all
development happens on the `develop` branch

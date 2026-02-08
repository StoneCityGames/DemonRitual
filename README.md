# DemonRitual

## Notes
Before merging anything, make sure your `.git/config` file contains the following lines:
```git
[merge]
  tool = unityyamlmerge

[mergetool "unityyamlmerge"]
  trustExitCode = false
  cmd = '<path-to-UnityYAMLMerge>' merge -p "$BASE" "$REMOTE" "$LOCAL" "$MERGED"
```

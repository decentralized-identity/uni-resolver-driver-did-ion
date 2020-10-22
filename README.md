[![Build Status](https://bmurdoch.visualstudio.com/uni-resolver-driver-did-ion/_apis/build/status/decentralized-identity.uni-resolver-driver-did-ion?branchName=master)](https://bmurdoch.visualstudio.com/uni-resolver-driver-did-ion/_build/latest?definitionId=1&branchName=master) ![Azure DevOps tests](https://img.shields.io/azure-devops/tests/bmurdoch/uni-resolver-driver-did-ion/1) ![Azure DevOps coverage](https://img.shields.io/azure-devops/coverage/bmurdoch/uni-resolver-driver-did-ion/1)

# uni-resolver-driver-did-ion
Universal Resolver Driver for Identity Overlay Network (ION) DIDs

![DIF Logo](https://raw.githubusercontent.com/decentralized-identity/universal-resolver/master/docs/logo-dif.png)

# Universal Resolver Driver: driver:ion

This is a [Universal Resolver](https://github.com/decentralized-identity/universal-resolver/) driver for **did:ion** identifiers.

## Specifications

* [Decentralized Identifiers](https://w3c.github.io/did-core/)
* [DID Method Specification](https://github.com/decentralized-identity/ion)

## Example DIDs

```
did:ion:EiC5-1uBg-YC2DvQRbI6eihDvk7DOYaQ08OB0I3jCe9Ydg:eyJkZWx0YSI6eyJwYXRjaGVzIjpbeyJhY3Rpb24iOiJyZXBsYWNlIiwiZG9jdW1lbnQiOnsicHVibGljX2tleXMiOlt7ImlkIjoiYW55U2lnbmluZ0tleUlkIiwiandrIjp7ImNydiI6InNlY3AyNTZrMSIsImt0eSI6IkVDIiwieCI6ImFHc01HMHU5Rlg2STU0cGVJS3FZb2tqblFQR2hMVVlUT1FOYzNuT3ZFMVEiLCJ5IjoiZmppbHFoZVdRWWtITkU3MHNoTVJ5TURyWnA4RUdDZkVfYUwzaC15Sm1RQSJ9LCJwdXJwb3NlIjpbImF1dGgiLCJnZW5lcmFsIl0sInR5cGUiOiJFY2RzYVNlY3AyNTZrMVZlcmlmaWNhdGlvbktleTIwMTkifV0sInNlcnZpY2VfZW5kcG9pbnRzIjpbeyJlbmRwb2ludCI6Imh0dHA6Ly9hbnkuZW5kcG9pbnQiLCJpZCI6ImFueVNlcnZpY2VFbmRwb2ludElkIiwidHlwZSI6ImFueVR5cGUifV19fV0sInVwZGF0ZV9jb21taXRtZW50IjoiRWlERkM2RE9Ed0JNeG5kX19oMTFSeDRObjFlOHpubFlPUjJhLVBqeUNva2NGZyJ9LCJzdWZmaXhfZGF0YSI6eyJkZWx0YV9oYXNoIjoiRWlBbExNMC1qem1DWi1FcElVZ0laQ2piWk5yMDFfVVBMbnd5MHdfT3I0Rks0dyIsInJlY292ZXJ5X2NvbW1pdG1lbnQiOiJFaUJDNGhTMVVHeVNnTmYzbWFMdnNKRUpxX05aQUlKa0pndTNKMTJMeGNESE93In19
```

## Build and Run (Docker)
```
docker build --pull --rm -f "./docker/Dockerfile" -t uniresolverdriverdidion:latest "src"
docker run --rm -d  -p 8080:8080/tcp uniresolverdriverdidion:latest
curl -X GET http://localhost:8080/1.0/identifiers/did:ion:EiC5-1uBg-YC2DvQRbI6eihDvk7DOYaQ08OB0I3jCe9Ydg:eyJkZWx0YSI6eyJwYXRjaGVzIjpbeyJhY3Rpb24iOiJyZXBsYWNlIiwiZG9jdW1lbnQiOnsicHVibGljX2tleXMiOlt7ImlkIjoiYW55U2lnbmluZ0tleUlkIiwiandrIjp7ImNydiI6InNlY3AyNTZrMSIsImt0eSI6IkVDIiwieCI6ImFHc01HMHU5Rlg2STU0cGVJS3FZb2tqblFQR2hMVVlUT1FOYzNuT3ZFMVEiLCJ5IjoiZmppbHFoZVdRWWtITkU3MHNoTVJ5TURyWnA4RUdDZkVfYUwzaC15Sm1RQSJ9LCJwdXJwb3NlIjpbImF1dGgiLCJnZW5lcmFsIl0sInR5cGUiOiJFY2RzYVNlY3AyNTZrMVZlcmlmaWNhdGlvbktleTIwMTkifV0sInNlcnZpY2VfZW5kcG9pbnRzIjpbeyJlbmRwb2ludCI6Imh0dHA6Ly9hbnkuZW5kcG9pbnQiLCJpZCI6ImFueVNlcnZpY2VFbmRwb2ludElkIiwidHlwZSI6ImFueVR5cGUifV19fV0sInVwZGF0ZV9jb21taXRtZW50IjoiRWlERkM2RE9Ed0JNeG5kX19oMTFSeDRObjFlOHpubFlPUjJhLVBqeUNva2NGZyJ9LCJzdWZmaXhfZGF0YSI6eyJkZWx0YV9oYXNoIjoiRWlBbExNMC1qem1DWi1FcElVZ0laQ2piWk5yMDFfVVBMbnd5MHdfT3I0Rks0dyIsInJlY292ZXJ5X2NvbW1pdG1lbnQiOiJFaUJDNGhTMVVHeVNnTmYzbWFMdnNKRUpxX05aQUlKa0pndTNKMTJMeGNESE93In19
```
## Driver configuration via config.json
```
{
  "DriverConfiguration": {
    "Resilience": {
      "EnableRetry": true, // Enables/disables the retry policy for transient HTTP errors including 429. Default is false.
      "EnableCircuitBreaking": true // Enables/disables the circuit breaking policy for transient HTTP errors. Default is false.
    },
    "Consensus": {
      "Model": "FirstWins", // [FirstWins, PartialAgreement, FullAgreement] specifies the consensus model when multiple nodes are provided. Not currently used.
      "InAgreement": "1" // Specifies the number of nodes when PartialAgreement is specified. Not currently used.
    },
    "Nodes": [
      {
        "Name": "default", // [Required] Name of the node - currently must be set to 'default'.
        "Uri": "https://beta.discover.did.microsoft.com/1.0/identifiers/", // [Required] Uri to the node with trailing slash.
        "TimeoutInMilliseconds": "5000",
        "Use": "Always", // [Always, Random] specifies whether requests should always be sent to the node or randomly, in a multi-node setup. Not currently used.
      }
    ]
  }
}
```

## Backlog
* Add support for HTTPS
* Add 'multi-request' to ION network
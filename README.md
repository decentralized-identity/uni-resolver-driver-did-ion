[![Build Status](https://bmurdoch.visualstudio.com/uni-resolver-driver-did-ion/_apis/build/status/decentralized-identity.uni-resolver-driver-did-ion?branchName=master)](https://bmurdoch.visualstudio.com/uni-resolver-driver-did-ion/_build/latest?definitionId=1&branchName=master) ![Azure DevOps tests](https://img.shields.io/azure-devops/tests/bmurdoch/uni-resolver-driver-did-ion/1) ![Azure DevOps coverage](https://img.shields.io/azure-devops/coverage/bmurdoch/uni-resolver-driver-did-ion/1)

# uni-resolver-driver-did-ion
Universal Resolver Driver for Identity Overlay Network (ION) DIDs v1

![DIF Logo](https://raw.githubusercontent.com/decentralized-identity/universal-resolver/master/docs/logo-dif.png)

# Universal Resolver Driver: driver-did-ion

This is a [Universal Resolver](https://github.com/decentralized-identity/universal-resolver/) driver for **did:ion** identifiers.

## Specifications

* [Decentralized Identifiers](https://w3c.github.io/did-core/)
* [DID Method Specification](https://github.com/decentralized-identity/ion)
* [ION v1](https://github.com/decentralized-identity/ion/tree/v1.0.0)

## Example DIDs

```
did:ion:EiD3DIbDgBCajj2zCkE48x74FKTV9_Dcu1u_imzZddDKfg
```

## Build and Run (Docker)
```
docker build --pull --rm -f "./.docker/Dockerfile" -t identityfoundation/driver-did-ion:latest .
docker run --rm -d  -p 8080:8080/tcp identityfoundation/driver-did-ion:lastest
curl -X GET http://localhost:8080/1.0/identifiers/did:ion:EiD3DIbDgBCajj2zCkE48x74FKTV9_Dcu1u_imzZddDKfg
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
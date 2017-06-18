# BackerUpper
Super original name for a application that backups specified folders to AWS Glacier

# Configuring

1. Create a `%HOMEPATH%\.aws\credential` file
```
[default]
aws_access_key_id = {access_key_id}
aws_secret_access_key = {secret_access_key}
```
2. Create IAM user with the `AmazonGlaciarFullAccess` policy permission
3. Update App.config to include location you would like backed up
```xml
<backerUpper uniqueClientId="test">
    <locations>
        <add filePath="C:\Users\USER_DIRECTORY\Desktop" backupFileName="MyDesktop" />
        <add filePath="C:\Users\USER_DIRECTORY\Documents" backupFileName="MyDocuments" />
    </locations>
</backerUpper>
```
4. Build project and run exe
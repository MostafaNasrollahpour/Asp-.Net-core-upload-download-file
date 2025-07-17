# How to download or upload a file in Asp .Net core using Minimal Apis?
This is a basic code to help you through this.

## How can you request to these endpoints?
### Upload:
>curl --request POST \
>--url http://localhost:5000/upload \
>--header 'Content-Type: multipart/form-data' \
>--form 'file=@"/path/to/your/file.txt"'

### Download:
>wget http://localhost:5000/download/yourfile.txt


###### Note: I am using linux, if you are on another os you have to request anotherway.

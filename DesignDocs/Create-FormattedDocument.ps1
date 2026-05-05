# PowerShell script to create a well-formatted Word document
param(
    [string]$InputFile = "Design_Document.md",
    [string]$OutputFile = "Design_Document_Enhanced.docx"
)

# Create Word application
$word = New-Object -ComObject Word.Application
$word.Visible = $false

try {
    # Create new document
    $document = $word.Documents.Add()
    
    # Read markdown content
    $content = Get-Content $InputFile -Raw
    
    # Split content by lines for better processing
    $lines = $content -split "`r?`n"
    
    # Process each line and add to document with formatting
    $selection = $word.Selection
    
    foreach ($line in $lines) {
        if ($line -match "^# (.+)") {
            # Main heading
            $selection.Font.Size = 18
            $selection.Font.Bold = $true
            $selection.Font.Color = 255  # Blue
            $selection.TypeText($matches[1])
            $selection.TypeParagraph()
            $selection.Font.Reset()
        }
        elseif ($line -match "^## (.+)") {
            # Sub heading
            $selection.Font.Size = 14
            $selection.Font.Bold = $true
            $selection.Font.Color = 0      # Black
            $selection.TypeText($matches[1])
            $selection.TypeParagraph()
            $selection.Font.Reset()
        }
        elseif ($line -match "^### (.+)") {
            # Sub-sub heading
            $selection.Font.Size = 12
            $selection.Font.Bold = $true
            $selection.TypeText($matches[1])
            $selection.TypeParagraph()
            $selection.Font.Reset()
        }
        elseif ($line -match "^\*\*(.+)\*\*`$") {
            # Bold text
            $selection.Font.Bold = $true
            $selection.TypeText($matches[1])
            $selection.TypeParagraph()
            $selection.Font.Reset()
        }
        elseif ($line -match "^```) {
            # Code block - skip for now
            continue
        }
        elseif ($line -match "^\|") {
            # Table row - convert to Word table format
            $selection.TypeText($line)
            $selection.TypeParagraph()
        }
        elseif ($line.Trim() -eq "") {
            # Empty line
            $selection.TypeParagraph()
        }
        else {
            # Regular text
            $selection.TypeText($line)
            $selection.TypeParagraph()
        }
    }
    
    # Save the document
    $fullPath = Join-Path (Get-Location) $OutputFile
    $document.SaveAs($fullPath)
    Write-Host "Enhanced Word document created: $OutputFile"
    
    # Try to create PDF
    try {
        $pdfPath = $OutputFile -replace "\.docx`$", ".pdf"
        $document.SaveAs((Join-Path (Get-Location) $pdfPath), 17)
        Write-Host "PDF document created: $pdfPath"
    }
    catch {
        Write-Host "PDF creation failed: $($_.Exception.Message)"
        Write-Host "You can manually save the Word document as PDF from within Microsoft Word."
    }
}
finally {
    # Clean up
    $document.Close()
    $word.Quit()
    [System.Runtime.Interopservices.Marshal]::ReleaseComObject($word) | Out-Null
}
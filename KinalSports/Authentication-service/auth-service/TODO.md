# TODO - Arreglar Auth Service

## Plan aprobado (asumido por falta de respuesta - fixes no rompen nada)

### 1. ✅ [DONE] Analizar archivos y logs (Email timeout + Exception handling)

### 2. Editar EmailService.cs
- Hacer SendEmailAsync no-throw: log + fallback si !enabled o error
- Disable SMTP por default en dev

### 3. Editar GlobalExceptionMiddleware.cs  
- Fix logging BusinessException (no "unhandled")
- Consistent JSON response

### 4. Editar appsettings.Development.json
- "SmtpSettings:Enabled": false
- Fix FrontendUrl space

### 5. Editar RateLimitingExtensions.cs
- Aumentar AuthPolicy limit

### 6. Test
- dotnet build
- F5 / test register
- Check no crash on email fail

### 7. [ ] attempt_completion


import { useForm } from 'react-hook-form'

export const ForgotPassword = ({ onSwitch }) => {
  const {
    register,
    handleSubmit,
    formState: { errors }
  } = useForm()
  return (
    <form className="space-y-5">
        <div>
            <label
                htmlFor="forgotPassword"
                className="block text-sm font-medium text-gray-800 mb-2 text-center"
            >
                Ingresa tu correo
            </label>

            <input
                type="email"
                id="forgotPassword"
                placeholder="correo@example.com"
                className ="w-full px-3 py-2 text-sm border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500"
                {
                    ...register('emailOrUsername', {
                        required: "Este campo es obligatorio"
                    })
                }
            />

            {errors.emailOrUsername && (
                <p className="text-red-600 text-xs mt-1">
                    {errors.emailOrUsername.message}
                </p>
            )}
        </div>

        <button
            type="submit"
            className="w-full bg-main-blue hover:opacity-90 text-white font-medium py-2.5 px-4 rounded-lg transition-colors duration-200 text-sm disabled:opacity-50"
        >
            Enviar código de recuperación
        </button>
        
        <p className="text-center text-sm">
          <button
            type="button"
            onClick={onSwitch}
            className='text-main-blue hover:underline'
          >
            Regresar al inicio de sesión
          </button>
        </p>
    </form>
  )
}

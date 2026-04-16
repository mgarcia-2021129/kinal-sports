import { Navbar } from './Navbar'
import { SideBar } from './SideBar'

export const DashboardContainer = ({user, onLogout, children}) => {
  return (
    <div className='min-h-screen bg-gray-50 flex flex-col'>
      <Navbar/>
      <div className='flex flex-1'>
        <SideBar/>
        <main classnName='flex-1 p-6'>
          {children}
        </main>
      </div>
    </div>
  )
}

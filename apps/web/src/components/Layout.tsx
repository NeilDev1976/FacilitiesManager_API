import { Outlet } from "react-router-dom";
import { Header } from "./Header.tsx";
import { Footer } from "./Footer.tsx";

const Layout = () => {
    return (
        <main className="flex flex-col min-h-screen">
            <Header />
            <div className="pt-24 flex-1">
                <Outlet />
            </div>
            <Footer />
        </main>
    );
}

export {Layout};
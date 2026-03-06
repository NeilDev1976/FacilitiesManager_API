import { createBrowserRouter } from "react-router-dom";
import Home from "./pages/Home.tsx";
import { Layout } from "./components/Layout.tsx";

const router = createBrowserRouter([
    {
        path: "/",
        element: <Layout />,
    },
]);

export { router };
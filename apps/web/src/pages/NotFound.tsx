import { Link } from "react-router-dom"

const NotFound = () => {
    return <>
    <header>404 - Not Found</header>
    <section>
        <Link to="/">Go Home</Link>
    </section>
    </>
}
 
export { NotFound }
import { useBusinesses } from 'src/hooks/BusinessHooks';

export default function BusinessesSearch() {
    const { businesses } = useBusinesses();
    return (
        <>
            <b>Businesses</b>
            {businesses.map(b => {
                return <p key={b.id}>{b.name}</p>
            })}
        </>
    )
}
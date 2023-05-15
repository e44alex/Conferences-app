import useSwr from "swr";
import { Session } from "@/shared/types/Conference";
import apiService from "@/shared/service/api";
import { SessionCard } from "@/components";

import "bootstrap/dist/css/bootstrap.css";

const SessionsPage = () => {
  const { data, error, isLoading } = useSwr<Session[]>(
    "/",
    apiService.getSessions
  );

  if (error) return <p>{error.error}</p>;
  if (isLoading) return <p>Loading...</p>;

  return (
    <>
      <h1>Sessions</h1>
      <div className="card-columns">
        {data &&
          data.map((session) => (
            <SessionCard key={session.id} session={session} />
          ))}
      </div>
    </>
  );
};

export default SessionsPage;
